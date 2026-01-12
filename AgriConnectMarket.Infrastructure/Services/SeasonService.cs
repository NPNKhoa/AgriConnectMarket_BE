using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class SeasonService(IUnitOfWork _uow)
    {
        public async Task<Result<ICollection<Season>>> GetAllSeasons(CancellationToken ct = default)
        {
            var farms = await _uow.SeasonRepository.ListAllAsync(ct);

            if (!farms.Any())
            {
                return Result<ICollection<Season>>.Success([]);
            }

            return Result<ICollection<Season>>.Success(farms.ToList());
        }

        public async Task<Result<Season>> GetSeasonById(Guid seasonId, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _uow.SeasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<Season>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            return Result<Season>.Success(season);
        }

        public async Task<Result<ICollection<Season>>> GetSeasonsByFarmIdAsync(Guid farmId, CancellationToken ct = default)
        {
            var seasons = await _uow.SeasonRepository.GetByFarmIdAsync(farmId, ct);

            if (!seasons.Any())
            {
                return Result<ICollection<Season>>.Success([]);
            }

            return Result<ICollection<Season>>.Success(seasons.ToList());
        }

        public async Task<Result<CreateSeasonResponseDto>> CreateSeason(CreateSeasonDto dto, CancellationToken ct = default)
        {
            var entity = new Season(dto.SeasonName, dto.SeasonDesc, dto.StartDate, dto.EndDate, dto.FarmId, dto.ProductId);

            await _uow.SeasonRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            var response = new CreateSeasonResponseDto()
            {
                SeasonName = entity.SeasonName,
                SeasonDesc = entity.SeasonDesc,
                Status = entity.Status,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };

            return Result<CreateSeasonResponseDto>.Success(response);
        }

        public async Task<Result<UpdateSeasonResponseDto>> UpdateSeasonAsync(Guid seasonId, UpdateSeasonDto dto, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _uow.SeasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<UpdateSeasonResponseDto>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            season.SeasonName = dto.SeasonName;
            season.SeasonDesc = dto.SeasonDesc;
            season.Status = dto.Status;
            season.StartDate = dto.StartDate;
            season.EndDate = dto.EndDate;

            await _uow.SeasonRepository.UpdateAsync(season, ct);
            await _uow.SaveChangesAsync(ct);

            var response = new UpdateSeasonResponseDto()
            {
                SeasonName = dto.SeasonName,
                SeasonDesc = dto.SeasonDesc,
                Status = dto.Status,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            return Result<UpdateSeasonResponseDto>.Success(response);
        }

        public async Task<Result<UpdateSeasonResponseDto>> UpdateSeasonStatusAsync(Guid seasonId, UpdateSeasonStatusDto dto, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _uow.SeasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<UpdateSeasonResponseDto>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            season.UpdateStatus(dto.newStatus);

            await _uow.SeasonRepository.UpdateAsync(season, ct);
            await _uow.SaveChangesAsync(ct);

            var response = new UpdateSeasonResponseDto()
            {
                SeasonName = season.SeasonName,
                SeasonDesc = season.SeasonDesc,
                Status = season.Status,
                StartDate = season.StartDate,
                EndDate = season.EndDate
            };

            return Result<UpdateSeasonResponseDto>.Success(response);
        }

        public async Task<Result<Guid>> DeleteAsync(Guid seasonId, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _uow.SeasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<Guid>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            await _uow.SeasonRepository.DeleteAsync(season, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<Guid>.Success(seasonId);
        }

        public async Task<Result<Season>> CloseSeasonAsync(Guid seasonId, CancellationToken ct = default)
        {
            Guard.AgainstNull(seasonId, nameof(seasonId));

            var season = await _uow.SeasonRepository.GetByIdAsync(seasonId, ct);

            if (season == null)
            {
                return Result<Season>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            season.Status = SeasonStatusEnums.HARVESTED;
            await _uow.SeasonRepository.UpdateAsync(season, ct);
            await _uow.SaveChangesAsync();

            return Result<Season>.Success(season);
        }
    }
}
