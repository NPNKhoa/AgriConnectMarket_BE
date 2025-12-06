using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Result;
using System.Text.Json;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class CareEventService(IUnitOfWork _uow, IDateTimeProvider _dateTimeProvider, IHashingStrategy _hasher)
    {
        public async Task<Result<CreateCareEventResultDto>> CreateCareEventAsync(CreateCareEventDto dto, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByIdAsync(dto.BatchId, ct);

            if (batch is null)
            {
                return Result<CreateCareEventResultDto>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            var eventType = await _uow.EventTypeRepository.GetByIdAsync(dto.EventTypeId);

            if (eventType is null)
            {
                return Result<CreateCareEventResultDto>.Fail(MessageConstant.EVENT_TYPE_NOT_FOUND);
            }

            var occurredAt = _dateTimeProvider.UtcNow;
            var payloadJson = JsonSerializer.Serialize(dto.Payload, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var lastBlock = await _uow.CareEventRepository.GetLastByBatchIdAsync(dto.BatchId, ct);

            string FIRST_HASH = "0x" + new string('0', 64);

            var canonical = _hasher.BuildCareEventCanonical(
                dto.BatchId.ToString(),
                eventType.EventTypeName,
                payloadJson,
                occurredAt.ToString("o"),
                lastBlock is not null ? lastBlock.Hash : FIRST_HASH
            );

            var hash = _hasher.ComputeHash(canonical);

            var careEvent = CareEvent.Create(dto.BatchId, dto.EventTypeId, occurredAt, payloadJson, dto.ImageUrl ?? "", hash, lastBlock is not null ? lastBlock.Hash : FIRST_HASH);

            await _uow.CareEventRepository.AddAsync(careEvent, ct);
            await _uow.SaveChangesAsync(ct);

            var response = new CreateCareEventResultDto()
            {
                Batch = batch,
                EventType = eventType,
                OccurredAt = occurredAt,
                Payload = dto.Payload
            };

            return Result<CreateCareEventResultDto>.Success(response);
        }

        public async Task<Result<IReadOnlyList<CareEventResponseDto>>> GetCareEventsByBatchAsync(Guid batchId, CancellationToken ct = default)
        {
            var events = await _uow.CareEventRepository.GetAllByBatchAsync(batchId, ct);

            if (!events.Any())
            {
                return Result<IReadOnlyList<CareEventResponseDto>>.Fail(MessageConstant.CARE_EVENT_NOTE_FOUND);
            }

            string FIRST_HASH = "0x" + new string('0', 64);

            string prevHash = FIRST_HASH;
            bool validChain = true;

            foreach (var e in events)
            {
                var canonical = _hasher.BuildCareEventCanonical(
                    batchId.ToString(),
                    e.EventType.EventTypeName,
                    e.Payload,
                    DateTime.SpecifyKind(e.OccurredAt, DateTimeKind.Utc).ToString("o"),
                    prevHash
                );

                var expected = _hasher.ComputeHash(canonical);

                if (e.PrevHash != prevHash || expected != e.Hash)
                {
                    validChain = false;
                    break;
                }

                prevHash = e.Hash;
            }

            if (!validChain)
            {
                return Result<IReadOnlyList<CareEventResponseDto>>.Fail(MessageConstant.INVALID_CHAIN);
            }

            var dtos = events
                .OrderBy(e => e.OccurredAt)
                .Select(e => new CareEventResponseDto
                {
                    Id = e.Id,
                    EventType = e.EventType.EventTypeName,
                    OccurredAt = e.OccurredAt,
                    Payload = e.Payload,
                    ImageUrl = e.ImageUrl ?? "",
                    BatchId = e.BatchId,
                    Hash = e.Hash,
                    PrevHash = e.PrevHash
                })
                .ToList();

            return Result<IReadOnlyList<CareEventResponseDto>>.Success(dtos);
        }

        public async Task<Result<bool>> VerifyBatchChainAsync(Guid batchId, CancellationToken ct = default)
        {
            var events = await _uow.CareEventRepository.GetAllByBatchAsync(batchId, ct);

            if (events.Count == 0)
                return Result<bool>.Fail("No events.");

            events = events.OrderBy(e => e.OccurredAt).ToList();

            string prevHash = string.Empty;

            foreach (var e in events)
            {
                var canonical = _hasher.BuildCareEventCanonical(
                    batchId.ToString(),
                    e.EventType.EventTypeName,
                    e.OccurredAt.ToString("o"),
                    e.Payload,
                    prevHash
                );

                var expected = _hasher.ComputeHash(canonical);

                if (expected != e.Hash || e.PrevHash != prevHash)
                    return Result<bool>.Success(false);

                prevHash = e.Hash;
            }

            return Result<bool>.Success(true);
        }

    }
}
