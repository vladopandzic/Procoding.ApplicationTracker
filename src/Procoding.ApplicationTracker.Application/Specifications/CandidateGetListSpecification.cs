using Ardalis.Specification;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Application.Specifications;

public class CandidateGetListSpecification : Specification<Candidate>
{
    public CandidateGetListSpecification(int? pageNumber, int? pageSize)
    {
        
        if (pageNumber.HasValue)
        {
            Query.Skip((pageNumber.Value - 1) * (pageSize ?? 1000));
        }
        if (pageSize.HasValue)
        {
            Query.Take(pageSize.Value);
        }
    }
}
