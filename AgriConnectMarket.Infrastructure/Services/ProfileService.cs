using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.ProfileSpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProfileService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<Domain.Entities.Profile>> UpdateProfile(Guid profileId, UpdateProfileDto dto, CancellationToken ct = default)
        {
            var existingProfile = await _uow.ProfileRepository.GetByIdAsync(profileId, ct);

            if (existingProfile is null)
            {
                return Result<Domain.Entities.Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            existingProfile.Fullname = dto.Fullname;
            existingProfile.Email = dto.Email;
            existingProfile.Phone = dto.Phone;

            await _uow.ProfileRepository.UpdateAsync(existingProfile, ct);

            return Result<Domain.Entities.Profile>.Success(existingProfile);
        }

        public async Task<Result<Domain.Entities.Profile>> UpdateAvatarAsync(Guid profileId, UpdateProfileDto dto, CancellationToken ct = default)
        {
            var existingProfile = await _uow.ProfileRepository.GetByIdAsync(profileId, ct);

            if (existingProfile is null)
            {
                return Result<Domain.Entities.Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            existingProfile.AvatarUrl = dto.AvatarUrl;

            await _uow.ProfileRepository.UpdateAsync(existingProfile, ct);

            return Result<Domain.Entities.Profile>.Success(existingProfile);
        }

        public async Task<Result<IEnumerable<Domain.Entities.Profile>>> GetFullListAsync(CancellationToken ct = default)
        {
            var profiles = await _uow.ProfileRepository.ListAllAsync(ct);

            if (!profiles.Any())
            {
                return Result<IEnumerable<Domain.Entities.Profile>>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            return Result<IEnumerable<Domain.Entities.Profile>>.Success(profiles.ToList());
        }

        public async Task<Result<IEnumerable<Domain.Entities.Profile>>> GetProfilesAsync(string searchTerm = "", CancellationToken ct = default)
        {
            var searchSpec = new FilterProfileBySearchTermSpecification(searchTerm);

            var profiles = await _uow.ProfileRepository.ListAsync(searchSpec, ct);

            if (!profiles.Any())
            {
                return Result<IEnumerable<Domain.Entities.Profile>>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            return Result<IEnumerable<Domain.Entities.Profile>>.Success(profiles);
        }

        public async Task<Result<Domain.Entities.Profile>> GetProfileById(Guid profileId, CancellationToken ct = default)
        {
            var existing = await _uow.ProfileRepository.GetByIdAsync(profileId, ct);

            if (existing is null)
            {
                return Result<Domain.Entities.Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            return Result<Domain.Entities.Profile>.Success(existing);
        }

        public async Task<Result<Domain.Entities.Profile>> GetMyProfile(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Domain.Entities.Profile>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<Domain.Entities.Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            return Result<Domain.Entities.Profile>.Success(profile);
        }
    }
}
