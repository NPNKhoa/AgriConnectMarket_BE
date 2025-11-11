using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProfileService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<Profile>> UpdateProfile(Guid profileId, UpdateProfileDto dto, CancellationToken ct = default)
        {
            var existingProfile = await _uow.ProfileRepository.GetByIdAsync(profileId, ct);

            if (existingProfile is null)
            {
                return Result<Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            existingProfile.Fullname = dto.Fullname;
            existingProfile.Email = dto.Email;
            existingProfile.Phone = dto.Phone;

            await _uow.ProfileRepository.UpdateAsync(existingProfile, ct);

            return Result<Profile>.Success(existingProfile);
        }

        public async Task<Result<Profile>> GetProfileById(Guid profileId, CancellationToken ct = default)
        {
            var existing = await _uow.ProfileRepository.GetByIdAsync(profileId, ct);

            if (existing is null)
            {
                return Result<Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            return Result<Profile>.Success(existing);
        }

        public async Task<Result<Profile>> GetMyProfile(CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<Profile>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = _currentUserService.UserId.Value;
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            return Result<Profile>.Success(profile);
        }
    }
}
