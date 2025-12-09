using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AddressService(IUnitOfWork _uow, ICurrentUserService _currentUserService)
    {
        public async Task<Result<IReadOnlyList<Address>>> GetAllAsync(CancellationToken ct = default)
        {
            var addresses = await _uow.AddressRepository.ListAllAsync(ct);

            if (!addresses.Any())
            {
                return Result<IReadOnlyList<Address>>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            return Result<IReadOnlyList<Address>>.Success(addresses);
        }

        public async Task<Result<CreateAddressResultDto>> CreateAddessAsync(CreateAddressDto dto, CancellationToken ct = default)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<CreateAddressResultDto>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = (Guid)_currentUserService.UserId;

            var existingDefault = await _uow.AddressRepository.GetDefaultAddressAsync(false, ct);

            if (existingDefault is not null)
            {
                existingDefault.IsDefault = false;
                await _uow.AddressRepository.UpdateAsync(existingDefault);
            }

            var user = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            var entity = new Address(dto.Province, dto.District, dto.Ward, dto.Detail, userId, dto.IsDefault)
            {
                Profile = user
            };

            var result = await _uow.AddressRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync();

            var resultDto = new CreateAddressResultDto()
            {
                AddressId = result.Id,
                Detail = result.Detail,
                Ward = result.Ward,
                District = result.District,
                Province = result.Province,
                IsDefault = result.IsDefault
            };

            return Result<CreateAddressResultDto>.Success(resultDto);
        }

        public async Task<Result<UpdateAddressResultDto>> UpdateAddressAsync(Guid addressId, UpdateAddressDto dto, CancellationToken ct = default)
        {
            var existingAddress = await _uow.AddressRepository.GetByIdAsync(addressId);

            if (existingAddress is null || existingAddress.IsDelete)
            {
                return Result<UpdateAddressResultDto>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            var existingDefault = await _uow.AddressRepository.GetDefaultAddressAsync(false, ct);

            if (existingDefault is not null)
            {
                existingDefault.IsDefault = false;
                await _uow.AddressRepository.UpdateAsync(existingDefault);
            }

            existingAddress.Province = dto.Province;
            existingAddress.District = dto.District;
            existingAddress.Ward = dto.Ward;
            existingAddress.Detail = dto.Detail;
            existingAddress.IsDefault = dto.IsDefault;

            await _uow.AddressRepository.UpdateAsync(existingAddress, ct);
            await _uow.SaveChangesAsync();

            var resultDto = new UpdateAddressResultDto()
            {
                AddressId = existingAddress.Id,
                Province = existingAddress.Province,
                District = existingAddress.District,
                Ward = existingAddress.Ward,
                Detail = existingAddress.Detail,
                IsDefault = existingAddress.IsDefault
            };

            return Result<UpdateAddressResultDto>.Success(resultDto);
        }

        public async Task<Result<Guid>> DeleteAddressAsync(Guid addressId, CancellationToken ct = default)
        {
            var existingAddress = await _uow.AddressRepository.GetByIdAsync(addressId);

            if (existingAddress is null || existingAddress.IsDelete)
            {
                return Result<Guid>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            existingAddress.IsDelete = true;
            await _uow.AddressRepository.UpdateAsync(existingAddress, ct);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(addressId);
        }

        // Token required
        public async Task<Result<IEnumerable<Address>>> GetUserAddressesAsync(CancellationToken ct)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<IEnumerable<Address>>.Fail(MessageConstant.NOT_AUTHENTICATED_USER);
            }

            var userId = (Guid)_currentUserService.UserId;
            var addresses = await _uow.AddressRepository.GetAddressesByProfileIdAsync(userId);

            if (!addresses.Any())
            {
                return Result<IEnumerable<Address>>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            return Result<IEnumerable<Address>>.Success(addresses);
        }
    }
}
