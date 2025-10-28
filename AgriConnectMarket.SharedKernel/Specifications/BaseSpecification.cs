using System.Linq.Expressions;

namespace AgriConnectMarket.SharedKernel.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>>? Criteria { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public Expression<Func<T, object>>? OrderByDesc { get; protected set; }
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public Expression<Func<T, object>>? Select { get; protected set; }

        // helper methods for concrete specs to call
        protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
        protected void ApplyCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;
        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression) => OrderByDesc = orderByDescExpression;
        protected void ApplyPaging(int skip, int take) { Skip = skip; Take = take; }
        protected void ApplySelect(Expression<Func<T, object>> selectExpression) => Select = selectExpression;
    }
}
