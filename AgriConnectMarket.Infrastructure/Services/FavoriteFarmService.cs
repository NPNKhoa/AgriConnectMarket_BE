using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class FavoriteFarmService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<IEnumerable<FavoriteFarm>>> GetMyFavoriteFarms(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<IEnumerable<FavoriteFarm>>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<IEnumerable<FavoriteFarm>>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var favoriteFarms = await _uow.FavoriteFarmRepository.GetByProfileAsync(userId, true, true, ct);

            if (!favoriteFarms.Any())
            {
                return Result<IEnumerable<FavoriteFarm>>.Fail(MessageConstant.FAVORITE_FARM_NOT_FOUND);
            }

            return Result<IEnumerable<FavoriteFarm>>.Success(favoriteFarms);
        }

        public async Task<Result<string>> AddToListAsync(UpdateFavoriteFarmDto dto, CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<string>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<string>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var existing = await _uow.FavoriteFarmRepository.GetByFKsAsync(profile.Id, dto.FarmId);

            if (existing is not null)
            {
                await _uow.FavoriteFarmRepository.DeleteAsync(existing, ct);
                await _uow.SaveChangesAsync(ct);

                return Result<string>.Fail("removed");
            }

            var entity = FavoriteFarm.Create(profile.Id, dto.FarmId);
            await _uow.FavoriteFarmRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync();

            return Result<string>.Success("added");
        }

        public async Task<Result<Guid>> RemoveFromListAsync(UpdateFavoriteFarmDto dto, CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Guid>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<Guid>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var existing = await _uow.FavoriteFarmRepository.GetByFKsAsync(profile.Id, dto.FarmId);

            if (existing is null)
            {
                return Result<Guid>.Fail(MessageConstant.FAVORITE_FARM_NOT_FOUND);
            }

            await _uow.FavoriteFarmRepository.DeleteAsync(existing, ct);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(existing.Id);
        }
    }
}
