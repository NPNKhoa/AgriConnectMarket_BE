using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProductBatchService(IUnitOfWork _uow, IBatchCodeGenerator _codeGenerator)
    {
        public async Task<Result<IEnumerable<ProductBatch>>> GetAllBatchesAsync(CancellationToken ct = default)
        {
            var batches = await _uow.ProductBatchRepository.ListAllAsync(ct);

            if (!batches.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batches);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchesBySeasonAsync(Guid seasonId, CancellationToken ct = default)
        {
            var batches = await _uow.ProductBatchRepository.GetBySeasonAsync(seasonId, true, ct);

            if (!batches.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batches);
        }

        public async Task<Result<ProductBatch>> GetBatchByIdAsync(Guid batchId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByIdAsync(batchId, true, ct);

            if (batch is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<ProductBatch>.Success(batch);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchByFarmIdAsync(Guid farmId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByFarmAsync(farmId);

            if (batch is null)
            {
                return Result<IEnumerable<ProductBatch>>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batch);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchesByFarmerAsync(Guid accountId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByFarmerAsync(accountId);

            if (batch is null)
            {
                return Result<IEnumerable<ProductBatch>>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batch);
        }

        public async Task<Result<CreateProductBatchResultDto>> CreateBatchAsync(CreateProductBatchDto dto, CancellationToken ct = default)
        {
            var entity = ProductBatch.Create(dto.SeasonId, dto.TotalYield, dto.AvailableQuantity, dto.PlantingDate, dto.Price, dto.Units);
            var season = await _uow.SeasonRepository.GetByIdAsync(dto.SeasonId, ct);

            if (season is null)
            {
                return Result<CreateProductBatchResultDto>.Fail(MessageConstant.SEASON_NOT_FOUND);
            }

            var farmId = season.FarmId;
            var farm = await _uow.FarmRepository.GetByIdAsync(farmId, ct);

            if (farm is null)
            {
                return Result<CreateProductBatchResultDto>.Fail(MessageConstant.FARM_NOT_FOUND);
            }

            if (farm.BatchCodePrefix is null)
            {
                return Result<CreateProductBatchResultDto>.Fail(MessageConstant.FARM_MISSING_PREFIX);
            }

            var codeString = await _codeGenerator.GenerateNextCodeAsync(farm.BatchCodePrefix, ct);

            entity.SetBatchCode(codeString);

            await _uow.ProductBatchRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            var responseDto = new CreateProductBatchResultDto()
            {
                BatchId = entity.Id,
                BatchCode = entity.BatchCode,
                SeasonId = entity.SeasonId,
                TotalYield = entity.TotalYield,
                Units = entity.Units,
                Price = entity.Price,
                AvailableQuantity = entity.AvailableQuantity,
                PlantingDate = entity.PlantingDate,
                HarvestDate = entity.HarvestDate
            };

            return Result<CreateProductBatchResultDto>.Success(responseDto);
        }

        public async Task<Result<ProductBatch>> UpdateInventoryAsync(Guid batchId, UpdateInventoryDto dto, CancellationToken ct = default)
        {
            var entity = await _uow.ProductBatchRepository.GetByIdAsync(batchId, ct);

            if (entity is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            entity.UpdateInventory(dto.soldQuantity);

            await _uow.ProductBatchRepository.UpdateAsync(entity);
            await _uow.SaveChangesAsync(ct);

            return Result<ProductBatch>.Success(entity);
        }
    }
}
