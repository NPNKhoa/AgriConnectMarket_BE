using AgriConnectMarket.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        // Applies the spec to the IQueryable and returns the ready-to-execute queryable
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> spec) where T : class
        {
            var query = inputQuery;

            // apply criteria
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // apply includes (EF's Include requires IIncludableQueryable, but for simple navigations this works)
            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            foreach (var chain in spec.IncludeChains)
            {
                var includable = query.Include(chain[0]);

                for (int i = 1; i < chain.Count; i++)
                    includable = includable.ThenInclude(chain[i]);

                query = includable;
            }

            // apply ordering
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            // apply paging
            if (spec.Skip.HasValue)
            {
                query = query.Skip(spec.Skip.Value);
            }
            if (spec.Take.HasValue)
            {
                query = query.Take(spec.Take.Value);
            }

            // Note: Select/projection handled in repository layer where necessary.
            return query;
        }
    }
}
