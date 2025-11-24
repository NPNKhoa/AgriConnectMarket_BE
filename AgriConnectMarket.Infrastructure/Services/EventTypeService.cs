using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class EventTypeService(IUnitOfWork _uow)
    {
        public async Task<Result<IEnumerable<CareEventType>>> GetAllAsync(CancellationToken ct = default)
        {
            var types = await _uow.EventTypeRepository.ListAllAsync(ct);

            if (!types.Any())
            {
                return Result<IEnumerable<CareEventType>>.Fail(MessageConstant.EVENT_TYPE_NOT_FOUND);
            }

            return Result<IEnumerable<CareEventType>>.Success(types);
        }

        public async Task<Result<CareEventType>> GetByIdAsync(Guid typeId, CancellationToken ct = default)
        {
            var type = await _uow.EventTypeRepository.GetByIdAsync(typeId, ct);

            if (type is null)
            {
                return Result<CareEventType>.Fail(MessageConstant.EVENT_TYPE_NOT_FOUND);
            }

            return Result<CareEventType>.Success(type);
        }

        public async Task<Result<CareEventType>> CreateAsync(CreateEventTypeDto dto, CancellationToken ct = default)
        {
            var type = CareEventType.Create(dto.TypeName, dto.TypeDesc);

            await _uow.EventTypeRepository.AddAsync(type, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<CareEventType>.Success(type);
        }

        public async Task<Result<Guid>> DeleteAsync(Guid typeId, CancellationToken ct = default)
        {
            var type = await _uow.EventTypeRepository.GetByIdAsync(typeId, ct);

            if (type is null)
            {
                return Result<Guid>.Fail(MessageConstant.EVENT_TYPE_NOT_FOUND);
            }

            await _uow.EventTypeRepository.DeleteAsync(type, ct);
            await _uow.SaveChangesAsync();

            return Result<Guid>.Success(type.Id);
        }
    }
}
