using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Queries.GetOneJobApplication;

public class GetOneJobApplicationQuery : IQuery<JobApplicationResponseDTO>
{
    public GetOneJobApplicationQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
