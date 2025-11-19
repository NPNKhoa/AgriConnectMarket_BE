using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.RequestDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.ProductsSpecs;
using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Guards;
using AgriConnectMarket.SharedKernel.Result;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class ProductService(IUnitOfWork _uow)
    {
        public async Task<Result<ICollection<Product>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var products = await _uow.ProductRepository.ListAllAsync(ct);

            if (!products.Any())
            {
                return Result<ICollection<Product>>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            return Result<ICollection<Product>>.Success(products.ToList());
        }


        public async Task<Result<IEnumerable<Product>>> GetProductsAsync(ProductQueryRequest query, CancellationToken ct = default)
        {
            ISpecification<Product> specs;

            if (query.searchTerm is not null)
            {
                specs = new FilterProductBySearchTerm(query.searchTerm);
            }

            if (query.categoryId is not null)
            {
                specs = new FilterProductsByCategory((Guid)query.categoryId);
            }

            if (query.location is not null)
            {
                specs = new FilterProductsByLocation(query.location);
            }

            specs = new SortProductsDefaultSpecs();

            var products = await _uow.ProductRepository.ListAsync(specs, ct);

            if (!products.Any())
            {
                return Result<IEnumerable<Product>>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            return Result<IEnumerable<Product>>.Success(products);
        }

        public async Task<Result<Product>> GetProductByIdAsync(Guid productId, CancellationToken ct = default)
        {
            Guard.AgainstNull(productId, nameof(productId));

            var product = await _uow.ProductRepository.GetByIdAsync(productId, ct);

            if (product is null)
            {
                return Result<Product>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            return Result<Product>.Success(product);
        }

        public async Task<Result<CreateProductResponseDto>> CreateProductAsync(CreateProductDto dto, CancellationToken ct = default)
        {
            var category = await _uow.CategoryRepository.GetByIdAsync(dto.CategoryId);

            if (category is null)
            {
                return Result<CreateProductResponseDto>.Fail(MessageConstant.CATEGORY_NOT_FOUND);
            }

            var product = new Product(dto.ProductName, dto.ProductAttribute, dto.CategoryId, dto.ProductDesc);

            await _uow.ProductRepository.AddAsync(product, ct);
            await _uow.SaveChangesAsync();

            var response = new CreateProductResponseDto()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                ProductAttribute = product.ProductAttribute,
                ProductDesc = product.ProductDesc,
                Category = category
            };

            return Result<CreateProductResponseDto>.Success(response);
        }

        public async Task<Result<UpdateProductResponseDto>> UpdateProductAsync(Guid productId, UpdateProductDto dto, CancellationToken ct = default)
        {
            Guard.AgainstNull(productId, nameof(productId));

            var category = await _uow.CategoryRepository.GetByIdAsync(dto.CategoryId, ct);

            if (category is null)
            {
                return Result<UpdateProductResponseDto>.Fail(MessageConstant.CATEGORY_NOT_FOUND);
            }

            var product = await _uow.ProductRepository.GetByIdAsync(productId, true, ct);

            if (product is null)
            {
                return Result<UpdateProductResponseDto>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            product.ProductName = dto.ProductName;
            product.ProductDesc = dto.ProductDesc;
            product.ProductAttribute = dto.ProductAttribute;
            product.CategoryId = dto.CategoryId;

            await _uow.ProductRepository.UpdateAsync(product, ct);
            await _uow.SaveChangesAsync();

            var response = new UpdateProductResponseDto()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                ProductAttribute = product.ProductAttribute,
                ProductDesc = product.ProductDesc,
                Category = category
            };

            return Result<UpdateProductResponseDto>.Success(response);
        }

        public async Task<Result<Guid>> DeleteProductAsync(Guid productId, CancellationToken ct = default)
        {
            var product = await _uow.ProductRepository.GetByIdAsync(productId, ct);

            if (product is null)
            {
                return Result<Guid>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            await _uow.ProductRepository.DeleteAsync(product);

            return Result<Guid>.Success(productId);
        }
    }
}
