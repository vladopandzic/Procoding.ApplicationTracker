using Ardalis.Specification;
using Procoding.ApplicationTracker.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Procoding.ApplicationTracker.Application.Specifications;

public class CandidateGetListSpecification : Specification<Candidate>
{
    public CandidateGetListSpecification(int? pageNumber, int? pageSize, List<Filter> filters, List<Sort> sort)
    {

        Query.ApplyFilters(filters.ToList());

        Query.ApplySorting(sort.ToList());

        if (pageNumber.HasValue)
        {
            Query.Skip((pageNumber.Value - 1) * (pageSize ?? 1000));
        }
        if (pageSize.HasValue)
        {
            Query.Take(pageSize.Value);
        }
    }

    public void ApplyCriteria(Expression<Func<Candidate, bool>> criteria)
    {
        Query.Where(criteria);
    }
}
