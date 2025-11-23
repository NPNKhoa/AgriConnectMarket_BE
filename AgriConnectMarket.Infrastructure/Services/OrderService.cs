using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class OrderService(IUnitOfWork _uow, IDateTimeProvider _dateTimeProvider, IOrderCodeGenerator _codeGenerator, ICurrentUserService _currentUserService)
    {
        public async Task<Result<IEnumerable<Order>>> GetAllOrdersAsync(CancellationToken ct = default)
        {
            var orders = await _uow.OrderRepository.ListAllAsync(ct);

            if (!orders.Any())
            {
                return Result<IEnumerable<Order>>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            return Result<IEnumerable<Order>>.Success(orders);
        }

        public async Task<Result<IEnumerable<Order>>> GetMyOrdersAsync(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<IEnumerable<Order>>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<IEnumerable<Order>>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var orders = await _uow.OrderRepository.GetOrderByProfileIdAsync(profile.Id, true, true, false, ct);

            if (!orders.Any())
            {
                return Result<IEnumerable<Order>>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            return Result<IEnumerable<Order>>.Success(orders);
        }

        public async Task<Result<IEnumerable<Order>>> GetFarmOrderAsync(Guid farmId, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<IEnumerable<Order>>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            var orders = await _uow.OrderRepository.GetOrdersByFarmIdAsync(farmId, true, true, true, ct);

            return Result<IEnumerable<Order>>.Success(orders);
        }

        public async Task<Result<Order>> GetOrderDetailAsync(Guid orderId, CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByIdAsync(orderId, ct);

            if (order is null)
            {
                return Result<Order>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            return Result<Order>.Success(order);
        }

        public async Task<Result<Order>> CreateOrder(CreateOrderDto dto, CancellationToken ct = default)
        {
            var customer = await _uow.ProfileRepository.GetByIdAsync(dto.CustomerId);

            if (customer is null)
            {
                return Result<Order>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            string orderCode = _codeGenerator.GenerateOrderCode();
            var order = Order.Create(dto.CustomerId, orderCode, _dateTimeProvider.UtcNow, dto.ShippingFee, OrderTypeConst.ORDER);

            foreach (var item in dto.OrderItems)
            {
                var batch = await _uow.ProductBatchRepository.GetByIdAsync(item.BatchId, ct);

                if (batch is null)
                {
                    return Result<Order>.Fail(MessageConstant.BATCH_NOT_FOUND);
                }

                order.AddItem(batch, item.Quantity);
            }

            await _uow.OrderRepository.AddAsync(order);
            await _uow.SaveChangesAsync();

            return Result<Order>.Success(order);
        }

        public async Task<Result<UpdateOrderStatusResponseDto>> UpdateOrderStatus(Guid orderId, UpdateOrderStatusDto dto, CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByIdAsync(orderId, ct);

            if (order is null)
            {
                return Result<UpdateOrderStatusResponseDto>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            DateTime? deliveredDate = dto.OrderStatus.Equals(OrderStatusEnum.DELIVERED) ? _dateTimeProvider.UtcNow : null;

            order.UpdateOrderStatus(dto.OrderStatus, deliveredDate);

            await _uow.OrderRepository.UpdateAsync(order, ct);
            await _uow.SaveChangesAsync();

            return Result<UpdateOrderStatusResponseDto>.Success(new UpdateOrderStatusResponseDto()
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus
            });
        }

        public async Task<Result<UpdateOrderStatusResponseDto>> CancelOrder(Guid orderId, CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                return Result<UpdateOrderStatusResponseDto>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            order.UpdateOrderStatus(OrderStatusEnum.CANCELED, null);

            await _uow.OrderRepository.UpdateAsync(order, ct);
            await _uow.SaveChangesAsync();

            return Result<UpdateOrderStatusResponseDto>.Success(new UpdateOrderStatusResponseDto()
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus
            });
        }

        public async Task<Result<CreatePreOrderResponseDto>> CreatePreOrder(CreatePreOrderDto dto, CancellationToken ct = default)
        {
            var customer = await _uow.ProfileRepository.GetByIdAsync(dto.CustomerId);

            if (customer is null)
            {
                return Result<CreatePreOrderResponseDto>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var product = await _uow.ProductRepository.GetByIdAsync(dto.ProductId);

            if (product is null)
            {
                return Result<CreatePreOrderResponseDto>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            string orderCode = _codeGenerator.GenerateOrderCode("PRE");
            var order = Order.Create(dto.CustomerId, orderCode, _dateTimeProvider.UtcNow, 0, OrderTypeConst.PREORDER);

            var preOrder = PreOrder.Create(order, dto.ProductId, dto.Quantity, dto.ExpectedReleaseDate, dto.Note!);

            await _uow.PreOrderRepository.AddAsync(preOrder);
            await _uow.SaveChangesAsync();

            var response = new CreatePreOrderResponseDto()
            {
                OrderCode = order.OrderCode,
                OrderType = order.OrderType,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                ExpectedReleaseDate = preOrder.ExpectedReleaseDate,
                PaymentStatus = order.PaymentStatus,
                PartiallyPaidAmount = preOrder.PartiallyPaidAmount,
                PaidDate = order.PaidDate,
                Note = preOrder.Note,
                Product = product,
                Quantity = preOrder.Quantity,
                Order = order
            };

            return Result<CreatePreOrderResponseDto>.Success(response);
        }
    }
}
