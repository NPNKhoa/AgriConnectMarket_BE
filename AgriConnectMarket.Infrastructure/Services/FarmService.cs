using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.FarmSpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class FarmService(IFarmRepository _farmRepository)
    {
        public async Task<Result<ICollection<Farm>>> GetAllFarms(FarmQuery? query, CancellationToken ct = default)
        {
            BaseSpecification<Farm> spec;

            if (query?.IsMallFarm == true)
            {
                spec = new FilterMallFarmSpecification();
            }

            if (query?.searchTerm is not null)
            {
                spec = new FilterFarmBySearchTermSpecification(query.searchTerm);
            }

            spec = new NameOrderedFarmsSpecification();

            var farms = await _farmRepository.ListAsync(spec, ct);

            if (!farms.Any())
            {
                return Result<ICollection<Farm>>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<ICollection<Farm>>.Success(farms.ToList());
        }

        public async Task<Result<Farm>> GetFarmById(Guid farmId, CancellationToken ct = default)
        {
            Guard.AgainstNull(farmId, nameof(farmId));

            var farm = await _farmRepository.GetByIdAsync(farmId, ct);

            if (farm == null)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<Farm>.Success(farm);
        }
    }
}
