using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.FarmSpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Normalization;
using AgriConnectMarket.SharedKernel.Result;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class FarmService(IUnitOfWork _uow, ICurrentUserService _currentUser)
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

            var farms = await _uow.FarmRepository.ListAsync(spec, ct);

            if (!farms.Any())
            {
                return Result<ICollection<Farm>>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<ICollection<Farm>>.Success(farms.ToList());
        }

        public async Task<Result<Farm>> GetFarmById(Guid farmId, CancellationToken ct = default)
        {
            Guard.AgainstNull(farmId, nameof(farmId));

            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, true, true, true, ct);

            if (farm is null || farm.IsDelete)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<Farm>.Success(farm);
        }

        public async Task<Result<Farm>> GetMyFarm(CancellationToken ct = default)
        {
            if (_currentUser.UserId is null)
            {
                return Result<Farm>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = (Guid)_currentUser.UserId;

            var user = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (user is null)
            {
                return Result<Farm>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            var farm = await _uow.FarmRepository.GetFarmByAccount(user.AccountId, true, true, true);

            if (farm == null || farm.IsDelete)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            if (farm.IsBanned)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_BANNED);
            }

            return Result<Farm>.Success(farm);
        }

        public async Task<Result<CreateFarmResponseDto>> CreateFarmAsync(CreateFarmDto dto, CancellationToken ct = default)
        {
            bool isPrefixConflicting = await isBatchCodePrefixConflicting(dto.BatchCodePrefix);

            if (isPrefixConflicting)
            {
                return Result<CreateFarmResponseDto>.Fail(MessageConstant.BATCH_CODE_CONFLICT);
            }

            var existingFarmer = await _uow.AuthenRepository.GetByIdAsync(dto.FarmerId);

            if (existingFarmer is null)
            {
                return Result<CreateFarmResponseDto>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            var address = new Address(dto.Province, dto.District, dto.Ward, dto.Detail, null, true);

            await _uow.AddressRepository.AddAsync(address);

            var farm = new Farm(dto.FarmName, dto.FarmDesc, dto.BatchCodePrefix, dto.BannerUrl, dto.Phone, dto.Area, dto.FarmerId)
            {
                Address = address
            };

            await _uow.FarmRepository.AddAsync(farm);
            await _uow.SaveChangesAsync();

            var responseDto = new CreateFarmResponseDto()
            {
                FarmId = farm.Id,
                FarmName = farm.FarmName,
                FarmDesc = farm.FarmDesc,
                BannerUrl = farm.BannerUrl,
                Area = farm.Area,
                Phone = farm.Phone
            };

            return Result<CreateFarmResponseDto>.Success(responseDto);
        }

        public async Task<Result<UpdateFarmResponseDto>> UpdateFarmAsync(Guid farmId, UpdateFarmDto dto, CancellationToken ct = default)
        {
            Guard.AgainstNull(farmId, nameof(farmId));

            var existingFarm = await _uow.FarmRepository.GetByIdAsync(farmId, true, true, true, ct);

            if (existingFarm is null)
            {
                return Result<UpdateFarmResponseDto>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            bool isAddressChanged = dto.Province is not null
                                    || dto.District is not null
                                    || dto.Ward is not null
                                    || dto.Detail is not null;

            existingFarm.FarmName = dto.FarmName;
            existingFarm.FarmDesc = dto.FarmDesc;
            existingFarm.BatchCodePrefix = dto.BatchCodePrefix;
            existingFarm.Phone = dto.Phone;
            existingFarm.Area = dto.Area;
            existingFarm.BannerUrl = dto.BannerUrl;
            existingFarm.BatchCodePrefix = existingFarm.BatchCodePrefix;

            if (false) // if (isAddressChanged)
            {
                var address = await _uow.AddressRepository.GetByIdAsync(existingFarm.AddressId);

                if (address is null)
                {
                    return Result<UpdateFarmResponseDto>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
                }

                address.Province = dto.Province ?? address.Province;
                address.District = dto.District ?? address.District;
                address.Ward = dto.Ward ?? address.Ward;
                address.Detail = dto.Detail ?? address.Detail;

                await _uow.AddressRepository.UpdateAsync(address);
            }

            await _uow.FarmRepository.UpdateAsync(existingFarm);
            await _uow.SaveChangesAsync();

            var responseDto = new UpdateFarmResponseDto()
            {
                FarmId = existingFarm.Id,
                FarmName = existingFarm.FarmName,
                FarmDesc = existingFarm.FarmDesc,
                BannerUrl = existingFarm.BannerUrl,
                Area = existingFarm.Area,
                Phone = existingFarm.Phone,
                Province = existingFarm.Address.Province,
                District = existingFarm.Address.District,
                Ward = existingFarm.Address.Ward,
                Detail = existingFarm.Address.Detail
            };

            return Result<UpdateFarmResponseDto>.Success(responseDto);
        }

        public async Task<Result<Guid>> DeleteFarmAsync(Guid farmId, CancellationToken ct = default)
        {
            Guard.AgainstNull(farmId, nameof(farmId));

            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm == null)
            {
                return Result<Guid>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.IsDelete = true;
            await _uow.FarmRepository.UpdateAsync(farm);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(farm.Id);
        }

        public async Task<Result<Guid>> ToggleFarmBanned(Guid farmId, CancellationToken ct = default)
        {
            Guard.AgainstNull(farmId, nameof(farmId));

            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm == null)
            {
                return Result<Guid>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.ToggleFarmBanned();
            await _uow.FarmRepository.UpdateAsync(farm);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(farm.Id);
        }

        public async Task<Result<Farm>> AllowForSell(Guid farmId, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.AllowSelling();
            await _uow.FarmRepository.UpdateAsync(farm);
            await _uow.SaveChangesAsync();

            return Result<Farm>.Success(farm);
        }

        public async Task<Result<Farm>> MarkFarmAsMall(Guid farmId, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            farm.ConfirmMallFarm();
            await _uow.FarmRepository.UpdateAsync(farm);
            await _uow.SaveChangesAsync();

            return Result<Farm>.Success(farm);
        }

        public async Task<Result<Farm>> GetFarmsByAccountAsync(Guid accountId, CancellationToken ct = default)
        {
            var farm = await _uow.FarmRepository.GetFarmByAccount(accountId, true, true, true);

            if (farm is null)
            {
                return Result<Farm>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            return Result<Farm>.Success(farm);
        }

        public async Task<Result<IEnumerable<FeaturedFarmResponseDto>>> GetFeaturedFarms(CancellationToken ct = default)
        {
            var orders = await _uow.OrderItemRepository.ListAllAsync(ct);

            if (!orders.Any())
            {
                return Result<IEnumerable<FeaturedFarmResponseDto>>.Success([]);
            }

            var farms = orders.GroupBy(o =>
            {
                var farm = o.Batch.Season.Farm;

                return new FeaturedFarmResponseDto(farm.Id, farm.FarmName, farm.FarmDesc ?? "", farm.BannerUrl ?? "", farm.FarmName, farm.Phone ?? "", farm.IsConfirmAsMall);
            })
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList()
            .Take(5);

            return Result<IEnumerable<FeaturedFarmResponseDto>>.Success(farms);
        }


        // HELPER
        private async Task<bool> isBatchCodePrefixConflicting(string bathCodePrefix)
        {
            if (string.IsNullOrEmpty(bathCodePrefix))
            {
                return true;
            }

            var farms = await _uow.FarmRepository.ListAllAsync();

            return farms.Any(f => f.BatchCodePrefix is null ? false : f.BatchCodePrefix.Equals(Normalizer.NormalizeStringToUpper(bathCodePrefix)));
        }
    }
}
