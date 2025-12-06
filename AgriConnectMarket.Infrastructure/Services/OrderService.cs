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

        public async Task<Result<IEnumerable<Order>>> GetMyPreOrdersAsync(CancellationToken ct = default)
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
            var order = await _uow.OrderRepository.GetByIdAsync(orderId, true, true, true, ct);

            if (order is null)
            {
                return Result<Order>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            return Result<Order>.Success(order);
        }

        public async Task<Result<Order>> GetOrderByOrderCodeAsync(string orderCode, CancellationToken ct = default)
        {
            var order = await _uow.OrderRepository.GetByOrderCodeAsync(orderCode, true, true, true, ct);

            if (order is null)
            {
                return Result<Order>.Fail(MessageConstant.ORDER_NOT_FOUND);
            }

            return Result<Order>.Success(order);
        }

        public async Task<Result<CreateOrderResponseDto>> CreateOrder(CreateOrderDto dto, CancellationToken ct = default)
        {
            // 1. Load customer
            var customer = await _uow.ProfileRepository.GetByIdAsync(dto.CustomerId, ct);
            if (customer is null)
                return Result<CreateOrderResponseDto>.Fail(MessageConstant.PROFILE_NOT_FOUND);

            // 2. Load address
            var address = await _uow.AddressRepository.GetByIdAsync(dto.AddressId, ct);
            if (address is null)
                return Result<CreateOrderResponseDto>.Fail(MessageConstant.ADDRESS_NOT_FOUND);

            // 3. Create order
            string orderCode = _codeGenerator.GenerateOrderCode();
            var order = Order.Create(
                dto.CustomerId,
                dto.AddressId,
                orderCode,
                _dateTimeProvider.UtcNow,
                OrderTypeConst.ORDER,
                dto.ShippingFee
            );

            // 4. Add all items
            foreach (var item in dto.OrderItems)
            {
                var batch = await _uow.ProductBatchRepository.GetByIdAsync(item.BatchId, ct);
                if (batch is null)
                    return Result<CreateOrderResponseDto>.Fail(MessageConstant.BATCH_NOT_FOUND);

                order.AddItem(batch, item.Quantity);
            }

            // 5. Save
            await _uow.OrderRepository.AddAsync(order, ct);
            await _uow.SaveChangesAsync(ct);

            var newOrder = await _uow.OrderRepository.GetByIdAsync(order.Id, true, false, true, ct);

            // 6. Build response DTO
            var response = new CreateOrderResponseDto(
                OrderId: newOrder.Id,
                Customer: new ProfileDto(customer.Fullname, customer.Email, customer.Phone),
                Address: new AddressDto(address.Province, address.District, address.Ward, address.Detail),
                OrderCode: order.OrderCode,
                TotalPrice: order.TotalPrice,
                OrderDate: order.OrderDate,
                ShippingFee: order.ShippingFee,
                OrderStatus: order.OrderStatus,
                OrderType: order.OrderType,
                PaymentStatus: order.PaymentStatus,
                PaymentMethod: order.PaymentMethod,
                PaidDate: order.PaidDate,
                DeliveredDate: order.DeliveredDate,
                CreatedAt: order.CreatedAt,
                UpdatedAt: order.UpdatedAt,
                Transaction: order.Transaction is null
                    ? null
                    : new TransactionDto(
                        order.Transaction.TransactionRef,
                        order.Transaction.TransactionNo,
                        order.Transaction.BankCode,
                        order.Transaction.Amount,
                        order.Transaction.Status,
                        order.Transaction.CreatedAt
                    ),
                OrderItems: GroupOrderItems(newOrder.OrderItems)
            );

            return Result<CreateOrderResponseDto>.Success(response);
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
            var order = Order.Create(dto.CustomerId, dto.AddressId, orderCode, _dateTimeProvider.UtcNow, OrderTypeConst.PREORDER);

            var preOrder = PreOrder.Create(order, dto.ProductId, dto.Quantity, dto.Note!);

            await _uow.PreOrderRepository.AddAsync(preOrder);
            await _uow.SaveChangesAsync();

            var response = new CreatePreOrderResponseDto()
            {
                OrderCode = order.OrderCode,
                OrderType = order.OrderType,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
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

        /***
         * 
         */
        private IReadOnlyCollection<ItemGroupedByFarmDto> GroupOrderItems(IReadOnlyCollection<OrderItem> items)
        {
            return items
                .GroupBy(i => i?.Batch?.Season?.FarmId)
                .Select(g => new ItemGroupedByFarmDto
                {
                    FarmId = (Guid)g.Key!,
                    FarmName = g.First().Batch.Season.Farm.FarmName,
                    Items = g.Select(i => new CartItemDto
                    {
                        ItemId = i.Id,
                        BatchId = i.BatchId,
                        BatchCode = i.Batch.BatchCode.Value,
                        BatchImageUrls = i.Batch.ImageUrls.Select(i => i.ImageUrl).ToList(),
                        ProductName = i.Batch.Season.Product.ProductName,
                        CategoryName = i.Batch.Season.Product.Category.CategoryName,
                        SeasonName = i.Batch.Season.SeasonName,
                        BatchPrice = i.Batch.Price,
                        Quantity = i.Quantity,
                        Units = i.Batch.Units,
                        ItemPrice = i.UnitPrice,
                        SeasonStatus = i.Batch.Season.Status
                    }).ToList()
                })
                .ToList();
        }
    }
}
