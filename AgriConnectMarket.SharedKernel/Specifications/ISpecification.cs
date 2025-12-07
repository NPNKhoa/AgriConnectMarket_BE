using System.Linq.Expressions;

namespace AgriConnectMarket.SharedKernel.Specifications
{
    public interface ISpecification<T>
    {
        /*
         * Criteria
         * Includes
         * OrderBy
         * OrderByDesc
         * Skip
         * Take
         */

        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDesc { get; }
        int? Skip { get; }
        int? Take { get; }
    }

}
