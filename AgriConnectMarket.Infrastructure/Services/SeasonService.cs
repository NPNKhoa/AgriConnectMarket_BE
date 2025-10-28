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
    public class SeasonService(ISeasonRepository _seasonRepository)
    {
        public async Task<Result<ICollection<Season>>> GetAllSeasons(SeasonQuery? query, CancellationToken ct = default)
        {
            BaseSpecification<Season> spec;

            if (query?.searchTerm is not null)
            {
                spec = new FilterSeasonBySearchTermSpecification(query.searchTerm);
            }

            spec = new NameOrderedSeasonsSpecification();

            var farms = await _seasonRepository.ListAsync(spec, ct);

            if (!farms.Any())
            {
                return Result<ICollection<Season>>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<ICollection<Season>>.Success(farms.ToList());
        }

        public async Task<Result<Season>> GetSeasonById(Guid seasonId, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _seasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<Season>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<Season>.Success(season);
        }
    }
}
