using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.CategorySpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class CategoryService(IUnitOfWork _uow)
    {
        public async Task<Result<IEnumerable<Category>>> GetAllCategoryAsync(CancellationToken ct = default)
        {
            var spec = new ExcludeDeletedCategorySpecification();

            var result = await _uow.CategoryRepository.ListAsync(spec, ct);

            if (!result.Any())
            {
                return Result<IEnumerable<Category>>.Fail(MessageConstant.CATEGORY_NOT_FOUND);
            }

            return Result<IEnumerable<Category>>.Success(result);
        }

        public async Task<Result<CreateCategoryResultDto>> CreateCategory(CreateCategoryDto dto, CancellationToken ct = default)
        {
            var existingCategory = await _uow.CategoryRepository.GetByNameAsync(dto.CategortName);

            if (existingCategory is not null)
            {
                return Result<CreateCategoryResultDto>.Fail(MessageConstant.EXISTING_CATEGORY);
            }

            var entity = new Category(dto.CategortName, dto.CategoryDesc, dto.IllustractiveImageUrl);

            await _uow.CategoryRepository.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);

            var responseDto = new CreateCategoryResultDto()
            {
                CategortName = entity.CategoryName,
                CategoryDesc = entity.CategoryDesc,
                IllustractiveImageUrl = entity.IllustrativeImageUrl
            };


            return Result<CreateCategoryResultDto>.Success(responseDto);
        }

        public async Task<Result<UpdateCategoryResultDto>> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto dto, CancellationToken ct = default)
        {
            Guard.AgainstNull(categoryId, nameof(categoryId));

            var existing = await _uow.CategoryRepository.GetByIdAsync(categoryId);

            if (existing is null || existing.IsDelete)
            {
                return Result<UpdateCategoryResultDto>.Fail(MessageConstant.CATEGORY_NOT_FOUND);
            }

            existing.CategoryName = dto.CategortName;
            existing.CategoryDesc = dto.CategoryDesc;
            existing.IllustrativeImageUrl = dto.IllustractiveImageUrl;

            await _uow.CategoryRepository.UpdateAsync(existing, ct);
            await _uow.SaveChangesAsync(ct);

            var responseDto = new UpdateCategoryResultDto()
            {
                CategortName = existing.CategoryName,
                CategoryDesc = existing.CategoryDesc,
                IllustractiveImageUrl = existing.IllustrativeImageUrl
            };

            return Result<UpdateCategoryResultDto>.Success(responseDto);
        }

        public async Task<Result<Guid>> DeleteCategoryAsync(Guid categoryId, CancellationToken ct = default)
        {
            Guard.AgainstNull(categoryId, nameof(categoryId));

            var existing = await _uow.CategoryRepository.GetByIdAsync(categoryId);

            if (existing is null)
            {
                return Result<Guid>.Fail(MessageConstant.CATEGORY_NOT_FOUND);
            }

            existing.IsDelete = true;
            await _uow.CategoryRepository.UpdateAsync(existing, ct);
            await _uow.SaveChangesAsync(ct);

            return Result<Guid>.Success(categoryId);
        }
    }
}
