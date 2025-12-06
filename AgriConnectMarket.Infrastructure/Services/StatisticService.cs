using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class StatisticService(IUnitOfWork _uow)
    {
        //public async Task<Result<RevenuStatisticDto>> Revenue(Guid farmId, RevenueQuery query, CancellationToken ct = default)
        //{
        //    var spec = new FilterOrderByCompletedInYearSpec(farmId, query.year);

        //    var orders = await _uow.OrderRepository.ListAsync(spec, ct);

        //    if (!orders.Any())
        //    {
        //        return Result<RevenuStatisticDto>.Fail(MessageConstant.UNKNOWN_ERROR);
        //    }

        //    var res = orders.ToList().GroupBy(o => o.CreatedAt.Month.ToString())
        //        .Select(g => new RevenuStatisticDto(g.Key.ToString(), ));
        //}

        //public async Task<Result> TopCustomers(Guid farmId, CancellationToken ct = default)
        //{

        //}

        //public async Task<Result> SellingProducts(Guid farmId, CancellationToken ct = default)
        //{

        //}

        public async Task<Result<IEnumerable<TotalUserStatisticDto>>> TotalUsers(CancellationToken ct = default)
        {
            var users = await _uow.ProfileRepository.ListAllAsync(true, ct);

            if (!users.Any())
            {
                return Result<IEnumerable<TotalUserStatisticDto>>.Fail(MessageConstant.PROFILE_NOT_FOUND);
            }

            var response = users.ToList().GroupBy(u => u.Account.Role).Select(g =>
            {
                return new TotalUserStatisticDto(g.Key, g.Count());
            }).ToList();

            return Result<IEnumerable<TotalUserStatisticDto>>.Success(response);
        }

        public async Task<Result<IEnumerable<ProductPerCategoryDto>>> ProductsPerCategory(CancellationToken ct = default)
        {
            var products = await _uow.ProductRepository.ListAllAsync(true, ct);

            if (!products.Any())
            {
                return Result<IEnumerable<ProductPerCategoryDto>>.Fail(MessageConstant.PRODUCT_NOT_FOUND);
            }

            var response = products.ToList().GroupBy(p => p.Category.CategoryName).Select(g =>
            {
                return new ProductPerCategoryDto(g.Key, g.Count());
            });

            return Result<IEnumerable<ProductPerCategoryDto>>.Success(response);
        }

        // VIP cus
        // Top selling product

        // Admin
        // Total Users
        // product per caterogory
    }
}
