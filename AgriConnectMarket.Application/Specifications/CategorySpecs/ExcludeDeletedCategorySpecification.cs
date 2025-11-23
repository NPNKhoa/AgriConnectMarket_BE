using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Specifications;

namespace AgriConnectMarket.Application.Specifications.CategorySpecs
{
    public class ExcludeDeletedCategorySpecification : BaseSpecification<Category>
    {
        public ExcludeDeletedCategorySpecification()
        {
            ApplyCriteria(c => !c.IsDelete);
        }
    }
}
