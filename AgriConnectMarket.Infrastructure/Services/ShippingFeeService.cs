using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ShippingFeeService(IUnitOfWork _uow, IShippingService _shippingService)
    {
        public async Task<Result<decimal>> GetShippingFeeAsync(CalculateShippingFeeQuery query, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(query.farmId, true, false, false);

            if (farm is null)
            {
                return Result<decimal>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            var address = await _uow.AddressRepository.GetByIdAsync(query.addressId, ct);

            if (address is null)
            {
                return Result<decimal>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            var shippingFee = await _shippingService.CalculateShippingFeeAsync(farm.Address.Province, farm.Address.District, address.Province, address.District, query.weight);

            return Result<decimal>.Success(shippingFee);
        }
    }
}
