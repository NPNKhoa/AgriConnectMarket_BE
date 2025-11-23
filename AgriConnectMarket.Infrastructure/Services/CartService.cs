using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class CartService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<Cart>> GetCartByUser(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Cart>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<Cart>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var cart = await _uow.CartRepository.GetByProfileIdAsync(profile.Id, true, false, ct);

            if (cart is null)
            {
                cart = Cart.InitCart(profile.Id);
            }

            return Result<Cart>.Success(cart);
        }

        public async Task<Result<CartItem>> AddToCartAsync(CreateCartItemDto dto, CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<CartItem>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<CartItem>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var cart = await _uow.CartRepository.GetByIdAsync(dto.CartId, ct);

            if (cart is null)
            {
                cart = Cart.InitCart(profile.Id);
            }

            var batch = await _uow.ProductBatchRepository.GetByIdAsync(dto.BatchId);

            if (batch is null)
            {
                return Result<CartItem>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            decimal itemPrice = batch.Price * dto.Quantity;

            var cartItem = CartItem.Create(dto.CartId, dto.BatchId, dto.Quantity, itemPrice);
            await _uow.CartItemRepository.AddAsync(cartItem);
            await _uow.SaveChangesAsync();

            return Result<CartItem>.Success(cartItem);
        }

        public async Task<Result<CartItem>> UpdateCartItemAsync(Guid cartId, UpdateCartItemDto dto, CancellationToken ct = default)
        {
            var cart = await _uow.CartRepository.GetByIdAsync(cartId, ct);

            if (cart is null)
            {
                return Result<CartItem>.Fail(MessageConstant.CART_NOT_INIT);
            }

            var batch = await _uow.ProductBatchRepository.GetByIdAsync(dto.BatchId);

            if (batch is null)
            {
                return Result<CartItem>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            var item = await _uow.CartItemRepository.GetByCartAndBatchAsync(cartId, batch.Id, ct);

            cart.TotalPrice -= item.ItemPrice;

            item.Quantity = dto.Quantity;
            item.ItemPrice = dto.Quantity * batch.Price;

            cart.TotalPrice += item.ItemPrice;

            await _uow.CartRepository.UpdateAsync(cart);
            await _uow.CartItemRepository.UpdateAsync(item);

            await _uow.SaveChangesAsync();

            return Result<CartItem>.Success(item);
        }

        public async Task<Result<Guid>> DeleteItemFromCartAsync(Guid cartItemId, CancellationToken ct = default)
        {
            var item = await _uow.CartItemRepository.GetByIdAsync(cartItemId);

            if (item is null)
            {
                return Result<Guid>.Fail(MessageConstant.CART_ITEM_NOT_FOUND);
            }

            await _uow.CartItemRepository.DeleteAsync(item);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(cartItemId);
        }
    }
}
