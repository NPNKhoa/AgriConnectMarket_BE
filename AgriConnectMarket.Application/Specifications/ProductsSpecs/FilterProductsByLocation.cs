using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.ProductsSpecs
{
    public sealed class FilterProductsByLocation : BaseSpecification<Product>
    {
        public FilterProductsByLocation(string location = "")
        {
            string normalizedLocationString = location.Trim().ToLower();

            ApplyCriteria(p => p.Season.Farm.Area.ToLower().Contains(normalizedLocationString)
                            || p.Season.Farm.Address.Province.ToLower().Contains(normalizedLocationString)
                            || p.Season.Farm.Address.District.ToLower().Contains(normalizedLocationString)
                            || p.Season.Farm.Address.Ward.ToLower().Contains(normalizedLocationString)
                            || p.Season.Farm.Address.Detail.ToLower().Contains(normalizedLocationString));
        }
    }
}
