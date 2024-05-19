using Ardalis.Specification;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Application.Specifications;

public sealed class JobApplicationSourceByIdSpecification : Specification<JobApplicationSource>
{
    public JobApplicationSourceByIdSpecification(Guid id)
    {
        Query.Where(x => x.Id == id);
    }
}
