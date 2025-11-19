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
            var cart = await _uow.CartRepository.GetByIdAsync(dto.CartId, ct);

            if (cart is null)
            {
                return Result<CartItem>.Fail(MessageConstant.CART_NOT_INIT);
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
    }
}
