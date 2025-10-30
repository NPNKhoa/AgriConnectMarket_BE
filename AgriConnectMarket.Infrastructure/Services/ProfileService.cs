using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProfileService(IProfileRepository _profileRepository)
    {
        public async Task<Result<Profile>> UpdateProfile(Guid profileId, UpdateProfileDto dto)
        {
            var existingProfile = await _profileRepository.GetByIdAsync(profileId);

            if (existingProfile is null)
            {
                return Result<Profile>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var entity = new Profile(dto.Fullname, dto.Email, dto.Phone, existingProfile.AccountId, "");

            await _profileRepository.UpdateAsync(entity);

            return Result<Profile>.Success(entity);
        }
    }
}
