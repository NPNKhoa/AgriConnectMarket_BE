using AgriConnectMarket.Application.DTOs.QueryDtos;
using AgriConnectMarket.Application.DTOs.ResponseDtos;
using AgriConnectMarket.Application.Interfaces;
using AgriConnectMarket.Application.Specifications.OrderSpecs;
using AgriConnectMarket.SharedKernel.Constants;
using AgriConnectMarket.SharedKernel.Result;

namespace AgriConnectMarket.Infrastructure.Services
{
    public class StatisticService(IUnitOfWork _uow)
    {
        public async Task<Result<IEnumerable<RevenuStatisticDto>>> Revenue(
            Guid farmId,
            RevenueQuery query,
            CancellationToken ct = default)
        {
            var spec = new FilterOrderByCompletedInYearSpec(farmId, query.year);
            var orders = await _uow.OrderRepository.ListAsync(spec, ct);

            var allMonths = Enumerable.Range(1, 12)
                .ToDictionary(m => m.ToString("00"), m => 0m);

            var revenueByMonth = orders
                .GroupBy(o => o.CreatedAt.Month.ToString("00"))
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(o => o.TotalPrice)
                );

            foreach (var item in revenueByMonth)
            {
                allMonths[item.Key] = item.Value;
            }

            var result = allMonths
                .Select(m => new RevenuStatisticDto(m.Key, m.Value))
                .OrderBy(m => m.month)
                .ToList();

            return Result<IEnumerable<RevenuStatisticDto>>.Success(result);
        }

        public async Task<Result<IEnumerable<TopCustomerStatsDto>>> TopCustomers(Guid farmId, CancellationToken ct = default)
        {
            var spec = new FilterPaidOrderByFarmSpecification(farmId);

            var orders = await _uow.OrderRepository.ListAsync(spec, ct);

            if (!orders.Any())
            {
                return Result<IEnumerable<TopCustomerStatsDto>>.Success([]);
            }

            var res = orders
                .GroupBy(o =>
                    new BriefCustomerInfoDto(
                        o.Customer.Id,
                        o.Customer.Email,
                        o.Customer.Fullname
                    )
                )
                .Select(g =>
                    new TopCustomerStatsDto(g.Key, g.Count())
                )
                .OrderByDescending(t => t.Amount)
                .Take(5)
                .ToList();

            return Result<IEnumerable<TopCustomerStatsDto>>.Success(res);
        }

        public async Task<Result<IEnumerable<BestSellingProductStatDto>>> BestSellingProducts(Guid farmId, CancellationToken ct = default)
        {
            var items = await _uow.OrderItemRepository.ListAsync(new FilterOrderItemInFarmAndPaidOrderSpecification(farmId), true, ct);

            if (!items.Any())
            {
                return Result<IEnumerable<BestSellingProductStatDto>>.Success([]);
            }

            var res = items
                .GroupBy(i =>
                    new BriefProductDto(i.Batch.Season.Product.Id, i.Batch.Season.Product.ProductName)
                )
                .Select(g => new BestSellingProductStatDto(g.Key, g.Count()))
                .OrderByDescending(b => b.amount)
                .Take(5)
                .ToList();

            return Result<IEnumerable<BestSellingProductStatDto>>.Success(res);
        }

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
