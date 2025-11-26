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

            var canonical = _hasher.BuildCareEventCanonical(dto.BatchId.ToString(), eventType.EventTypeName, dto.Payload, occurredAt.ToString("o"), lastBlock.Hash);
            var hash = _hasher.ComputeHash(canonical);

            var careEvent = CareEvent.Create(dto.BatchId, dto.EventTypeId, occurredAt, payloadJson, hash, lastBlock.PrevHash);

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

            events = events.OrderBy(e => e.OccurredAt).ToList();

            string prevHash = string.Empty;
            bool validChain = true;

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
                    EventTypeId = e.EventTypeId,
                    OccurredAt = e.OccurredAt,
                    Payload = e.Payload,
                    Hash = e.Hash,
                    PrevHash = e.PrevHash
                })
                .ToList();

            return Result<IReadOnlyList<CareEventResponseDto>>.Success(dtos);
        }

        //public async Task<Result<bool>> VerifyEventAsync(Guid careEventId, CancellationToken ct = default)
        //{
        //    var ev = await _uow.CareEventRepository.GetByIdAsync(careEventId);

        //    if (ev is null)
        //        return Result<bool>.Fail("Event not found.");

        //    var prev = await _uow.CareEventRepository.GetPreviousEventAsync(ev, ct);

        //    string expectedPrevHash = prev?.Hash ?? string.Empty;

        //    var canonical = _hasher.BuildCareEventCanonical(
        //        ev.BatchId.ToString(),
        //        ev.EventType.EventTypeName,
        //        ev.OccurredAt.ToString("o"),
        //        ev.PayloadJson,
        //        expectedPrevHash
        //    );

        //    var expectedHash = _hasher.ComputeHash(canonical);

        //    bool ok = expectedHash == ev.Hash && ev.PreviousHash == expectedPrevHash;

        //    return Result<bool>.Success(ok);
        //}

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
