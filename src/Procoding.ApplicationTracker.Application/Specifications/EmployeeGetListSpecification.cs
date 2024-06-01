using Ardalis.Specification;
using Procoding.ApplicationTracker.Application.Core.Query;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Application.Specifications;

public class EmployeeGetListSpecification : Specification<Employee>
{
    public EmployeeGetListSpecification(int? pageNumber, int? pageSize, List<Filter> filters, List<Sort> sort)
    {
        Query.ApplyPaging(pageNumber, pageSize);

        Query.ApplyFilters(filters.ToList());

        Query.ApplySorting(sort.ToList());

    }
}
