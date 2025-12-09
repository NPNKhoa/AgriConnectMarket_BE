using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class CartService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<CartResponseDto>> GetCartByUser(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<CartResponseDto>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<CartResponseDto>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var cart = await _uow.CartRepository.GetByProfileIdAsync(profile.Id, true, true, ct);

            if (cart is null)
            {
                cart = Cart.InitCart(profile.Id);

                await _uow.CartRepository.AddAsync(cart);
                await _uow.SaveChangesAsync();
            }

            var grouped = cart.CartItems?
                .GroupBy(i => i.Batch.Season.Farm.Id)
                .Select(g =>
                {
                    var firstItem = g.First();
                    var farm = firstItem.Batch.Season.Farm;

                    return new ItemGroupedByFarmDto
                    {
                        FarmId = farm.Id,
                        FarmName = farm.FarmName,
                        Items = g.Select(i =>
                        {
                            var batch = i.Batch;
                            var season = batch.Season;
                            var product = season.Product;

                            return new CartItemDto
                            {
                                ItemId = i.Id,
                                ProductName = product.ProductName,
                                CategoryName = product.Category.CategoryName,
                                SeasonName = season.SeasonName,
                                SeasonStatus = season.Status,
                                BatchId = batch.Id,
                                BatchCode = batch.BatchCode.Value,
                                BatchImageUrls = batch.ImageUrls.Select(item => item.ImageUrl).ToList(),
                                BatchPrice = batch.Price,
                                Units = batch.Units,
                                Quantity = i.Quantity,
                                ItemPrice = i.ItemPrice
                            };
                        }).ToList()
                    };
                }).ToList() ?? new List<ItemGroupedByFarmDto>();

            var cartResponse = new CartResponseDto()
            {
                CartId = cart.Id,
                Email = cart.Customer.Email,
                Fullname = cart.Customer.Fullname,
                Phone = cart.Customer.Phone!,
                TotalPrice = cart.TotalPrice,
                CartItems = grouped
            };

            return Result<CartResponseDto>.Success(cartResponse);
        }

        public async Task<Result<CartItem>> AddToCartAsync(CreateCartItemDto dto, CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
                return Result<CartItem>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
                return Result<CartItem>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);

            var cart = await _uow.CartRepository.GetByIdAsync(dto.CartId, true, false, false, ct);

            if (cart is null)
            {
                cart = Cart.InitCart(profile.Id);
                await _uow.CartRepository.AddAsync(cart);
                await _uow.SaveChangesAsync();
            }

            var batch = await _uow.ProductBatchRepository.GetByIdAsync(dto.BatchId, false, true, ct);

            if (batch is null)
                return Result<CartItem>.Fail(MessageConstant.BATCH_NOT_FOUND);

            decimal unitPrice = batch.Price;

            var existing = cart.CartItems!.FirstOrDefault(i => i.BatchId == dto.BatchId);

            if (existing is not null)
            {
                existing.Quantity += dto.Quantity;
                existing.ItemPrice = existing.Quantity * unitPrice;

                await _uow.CartItemRepository.UpdateAsync(existing);
                await _uow.SaveChangesAsync();

                return Result<CartItem>.Success(existing);
            }

            var newItem = CartItem.Create(cart.Id, dto.BatchId, dto.Quantity, unitPrice * dto.Quantity);

            await _uow.CartItemRepository.AddAsync(newItem);
            await _uow.SaveChangesAsync();

            return Result<CartItem>.Success(newItem);
        }

        public async Task<Result<CartItem>> UpdateCartItemAsync(Guid cartId, UpdateCartItemDto dto, CancellationToken ct = default)
        {
            var cart = await _uow.CartRepository.GetByIdAsync(cartId, true, false, false, ct);

            if (cart is null)
            {
                return Result<CartItem>.Fail(MessageConstant.CART_NOT_INIT);
            }

            var batch = await _uow.ProductBatchRepository.GetByIdAsync(dto.BatchId, ct);

            if (batch is null)
            {
                return Result<CartItem>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            var item = await _uow.CartItemRepository.GetByCartAndBatchAsync(cartId, batch.Id, ct);

            cart.UpdateCartItem(item, batch, dto.Quantity);

            await _uow.CartRepository.UpdateAsync(cart);

            await _uow.SaveChangesAsync();

            var responseDto = new UpdateCartResponseDto(item.Id, item.Quantity, item.ItemPrice, cart.TotalPrice);

            return Result<CartItem>.Success(item);
        }

        public async Task<Result<Guid>> DeleteItemFromCartAsync(Guid cartItemId, CancellationToken ct = default)
        {
            var item = await _uow.CartItemRepository.GetByIdAsync(cartItemId, ct);

            if (item is null)
            {
                return Result<Guid>.Fail(MessageConstant.CART_ITEM_NOT_FOUND);
            }

            var cart = await _uow.CartRepository.GetByItemIdAsync(cartItemId, true, ct);

            if (cart is null)
            {
                return Result<Guid>.Fail(MessageConstant.CART_NOT_INIT);
            }

            cart.DeleteFromCart(item);

            await _uow.CartRepository.UpdateAsync(cart, ct);
            await _uow.CartItemRepository.DeleteAsync(item, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<Guid>.Success(cartItemId);
        }

        public async Task<Result<Cart>> DeleteAllItemsAsync(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
                return Result<Cart>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, true, ct);

            if (profile is null)
                return Result<Cart>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);

            var cart = await _uow.CartRepository.GetByIdAsync(profile.Cart.Id, true, false, false, ct);

            cart.DeleteAllFromCart();

            await _uow.CartRepository.UpdateAsync(cart, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<Cart>.Success(cart);
        }
    }
}
