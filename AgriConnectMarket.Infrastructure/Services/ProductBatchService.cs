using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.ProductBatchSpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Interfaces;
using AgriConnectMarket.SharedKernel.Result;
using AgriConnectMarket.SharedKernel.Specifications;
using System.Data;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProductBatchService(IUnitOfWork _uow, IBatchCodeGenerator _codeGenerator, IDateTimeProvider _dateTimeProvider, IQrCodeGenerator _qrCodeGenerator, CareEventService _careEventService)
    {
        public async Task<Result<IEnumerable<ProductBatchResponseDto>>> GetAllBatchesAsync(CancellationToken ct = default)
        {
            var batches = await _uow.ProductBatchRepository.ListAllAsync(true, ct);

            if (!batches.Any())
            {
                return Result<IEnumerable<ProductBatchResponseDto>>.Success([]);
            }

            var responseDto = batches.ToList().Select(b =>
            {
                List<string> urls = b.ImageUrls.ToList().Select(i => i.ImageUrl).ToList();
                return new ProductBatchResponseDto(
                    b.Id,
                    b.BatchCode.Value,
                    b.Season.Product.ProductName,
                    b.Season.SeasonName,
                    b.Season.Farm.FarmName,
                    b.Season?.Product?.Category?.CategoryName,
                    b.CreatedAt,
                    b.PlantingDate,
                    b.HarvestDate,
                    b.TotalYield,
                    b.AvailableQuantity,
                    b.Price,
                    b.Units,
                    urls
                );
            }).ToList();

            return Result<IEnumerable<ProductBatchResponseDto>>.Success(responseDto);
        }

        public async Task<Result<IEnumerable<ProductBatchResponseDto>>> GetSellingBatches(ProductBatchQuery query, CancellationToken ct = default)
        {
            int pageNumber = query.pageNumber is not null ? (int)query.pageNumber : 1;
            int pageSize = query.pageSize is not null ? (int)query.pageSize : 10;

            int skip = (pageNumber - 1) * pageSize;

            ISpecification<ProductBatch> specs = new FilterProductBatchByPaginationSpecification(skip, pageSize);

            if (query.searchTerm is not null)
            {
                specs = new FilterProductBatchBySearchTermSpecification(query.searchTerm);
            }

            if (query.categoryId is not null)
            {
                specs = new FilterProductBatchByCategoryIdSpecification((Guid)query.categoryId);
            }

            if (query.isDesc is not null)
            {
                specs = new SortingProductBatchSpecification((bool)query.isDesc);
            }

            var batches = await _uow.ProductBatchRepository.ListAsync(specs, true, ct);

            if (!batches.Any())
            {
                return Result<IEnumerable<ProductBatchResponseDto>>.Success([]);
            }

            var responseDto = batches.Select(b =>
            {
                List<string> urls = b.ImageUrls.Select(i => i.ImageUrl).ToList();
                return new ProductBatchResponseDto(
                    b.Id,
                    b.BatchCode.Value,
                    b.Season?.Product?.ProductName,
                    b.Season?.SeasonName,
                    b.Season?.Product?.Category?.CategoryName,
                    b.Season?.Farm?.FarmName,
                    b.CreatedAt,
                    b.PlantingDate,
                    b.HarvestDate,
                    b.TotalYield,
                    b.AvailableQuantity,
                    b.Price,
                    b.Units,
                    urls
                );
            }).ToList();

            return Result<IEnumerable<ProductBatchResponseDto>>.Success(responseDto);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchesBySeasonAsync(Guid seasonId, CancellationToken ct = default)
        {
            var batches = await _uow.ProductBatchRepository.GetBySeasonAsync(seasonId, true, ct);

            if (!batches.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Success([]);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batches);
        }

        public async Task<Result<ProductBatch>> GetBatchByIdAsync(Guid batchId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByIdAsync(batchId, true, true, ct);

            if (batch is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            return Result<ProductBatch>.Success(batch);
        }

        public async Task<Result<ProductBatch>> StopSellingAsync(Guid batchId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByIdAsync(batchId, true, true, ct);

            if (batch is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            batch.StopSelling();

            await _uow.ProductBatchRepository.UpdateAsync(batch, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<ProductBatch>.Success(batch);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchByFarmIdAsync(Guid farmId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByFarmAsync(farmId, true, ct);

            if (!batch.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Success([]);
            }

            var responseDto = batch.Select(b =>
            {
                List<string> urls = b.ImageUrls.Select(i => i.ImageUrl).ToList();
                return new ProductBatchResponseDto(
                    b.Id,
                    b.BatchCode.Value,
                    b.Season?.Product?.ProductName,
                    b.Season?.SeasonName,
                    b.Season?.Product?.Category?.CategoryName,
                    b.Season?.Farm?.FarmName,
                    b.CreatedAt,
                    b.PlantingDate,
                    b.HarvestDate,
                    b.TotalYield,
                    b.AvailableQuantity,
                    b.Price,
                    b.Units,
                    urls
                );
            }).ToList();

            return Result<IEnumerable<ProductBatch>>.Success(batch);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetBatchesByFarmerAsync(Guid accountId, CancellationToken ct = default)
        {
            var batch = await _uow.ProductBatchRepository.GetByFarmerAsync(accountId);

            if (!batch.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Success([]);
            }

            return Result<IEnumerable<ProductBatch>>.Success(batch);
        }

        public async Task<Result<CreateProductBatchResultDto>> CreateBatchAsync(CreateProductBatchDto dto, CancellationToken ct = default)
        {
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

            var entity = ProductBatch.Create(dto.SeasonId, dto.TotalYield, dto.PlantingDate, dto.Units);

            var codeString = await _codeGenerator.GenerateNextCodeAsync(farm.BatchCodePrefix, ct);

            entity.SetBatchCode(codeString);
            foreach (var item in dto.ImageUrl)
            {
                entity.AddImage(new ProductBatchImage(entity.Id, item));
            }

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

        public async Task<Result<ProductBatch>> UpdateHarvestDateAsync(Guid batchId, UpdateBatchHarvestDateDto dto, CancellationToken ct = default)
        {
            var entity = await _uow.ProductBatchRepository.GetByIdAsync(batchId, ct);

            if (entity is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            var currentDate = _dateTimeProvider.UtcNow;
            entity.Harvest(currentDate, dto.totalYield);

            await _uow.ProductBatchRepository.UpdateAsync(entity);
            await _uow.SaveChangesAsync();

            return Result<ProductBatch>.Success(entity);
        }

        public async Task<Result<ProductBatch>> SellBatchAsync(Guid batchId, SellProductBatchRequestDto dto, CancellationToken ct = default)
        {
            var entity = await _uow.ProductBatchRepository.GetByIdAsync(batchId, ct);

            if (entity is null)
            {
                return Result<ProductBatch>.Fail(MessageConstant.BATCH_NOT_FOUND);
            }

            entity.Sell(dto.availableQuantity, dto.price);

            var generatedQrCodeUrl = await _qrCodeGenerator.GenerateAndUploadBatchQrAsync(batchId, ct);

            entity.VerificationQr = generatedQrCodeUrl;

            await _uow.ProductBatchRepository.UpdateAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<ProductBatch>.Success(entity);
        }

        public async Task<Result<IEnumerable<ProductBatch>>> GetRecommendedForUserAsync(Guid userId, CancellationToken ct)
        {
            var profile = await _uow.ProfileRepository.GetByIdAsync(userId, ct);

            if (profile is null)
            {
                return Result<IEnumerable<ProductBatch>>.Success([]);
            }

            var orders = await _uow.OrderRepository.GetOrderByProfileIdAsync(profile.Id, true, false, false, ct);

            if (!orders.Any())
            {
                return Result<IEnumerable<ProductBatch>>.Success([]);
            }

            var batches = await _uow.ProductBatchRepository.ListAllAsync(ct);

            var group = batches
                .Where(b => b.OrderItems.Any(i => i.Order.CustomerId == profile.Id))
                .GroupBy(b => new { b.Season.FarmId, b.Id })
                .Select(g => new { key = g.Key, count = g.Count() })
                .OrderByDescending(kv => kv.count)
                .ToList();

            IEnumerable<ProductBatch> res = new List<ProductBatch>();

            foreach (var g in group)
            {
                res.Append(batches.FirstOrDefault(b => b.Id == g.key.Id));
            }

            return Result<IEnumerable<ProductBatch>>.Success(res);
        }
    }
}
