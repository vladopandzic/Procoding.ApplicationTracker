using Ardalis.Specification;
using Procoding.ApplicationTracker.Application.Core.Query;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Application.Specifications;

public class CandidateGetListSpecification : Specification<Candidate>
{
    public CandidateGetListSpecification(int? pageNumber, int? pageSize, List<Filter> filters, List<Sort> sort)
    {
        Query.ApplyPaging(pageNumber, pageSize);

        Query.ApplyFilters(filters.ToList());

        Query.ApplySorting(sort.ToList());
     

    }
}
