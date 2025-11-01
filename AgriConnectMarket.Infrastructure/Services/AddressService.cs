﻿using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class AddressService(IAddressRepository _addressRepository, IProfileRepository _profileRepository, ICurrentUserService _currentUserService)
    {
        public async Task<Result<CreateAddressResultDto>> CreateAddessAsync(CreateAddressDto dto, CancellationToken ct = default)
        {
            var user = await _profileRepository.GetByIdAsync(dto.ProfileId, ct);

            if (user is null)
            {
                return Result<CreateAddressResultDto>.Fail(MessageConstant.PROFILE_ID_NOT_FOUND);
            }

            var entity = new Address(dto.Province, dto.District, dto.Ward, dto.Detail, dto.ProfileId, dto.IsDefault)
            {
                Profile = user
            };

            var result = await _addressRepository.AddAsync(entity, ct);

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
            var existingAddress = await _addressRepository.GetByIdAsync(addressId);

            if (existingAddress is null)
            {
                return Result<UpdateAddressResultDto>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            existingAddress.Province = dto.Province;
            existingAddress.District = dto.District;
            existingAddress.Ward = dto.Ward;
            existingAddress.Detail = dto.Detail;
            existingAddress.IsDefault = dto.IsDefault;

            await _addressRepository.UpdateAsync(existingAddress, ct);

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
            var existingAddress = await _addressRepository.GetByIdAsync(addressId);

            if (existingAddress is null)
            {
                return Result<Guid>.Fail(MessageConstant.ACCOUNT_NOT_FOUND);
            }

            await _addressRepository.DeleteAsync(existingAddress, ct);

            return Result<Guid>.Success(addressId);
        }

        // Token required
        public async Task<Result<IEnumerable<Address>>> GetUserAddressesAsync(CancellationToken ct)
        {
            if (_currentUserService.UserId is null)
            {
                return Result<IEnumerable<Address>>.Fail(MessageConstant.NOTE_AUTHENTICATED_USER);
            }

            var userId = (Guid)_currentUserService.UserId;
            var addresses = await _addressRepository.GetAddressesByProfileIdAsync(userId);

            if (!addresses.Any())
            {
                return Result<IEnumerable<Address>>.Fail(MessageConstant.ADDRESS_NOT_FOUND);
            }

            return Result<IEnumerable<Address>>.Success(addresses);
        }
    }
}
