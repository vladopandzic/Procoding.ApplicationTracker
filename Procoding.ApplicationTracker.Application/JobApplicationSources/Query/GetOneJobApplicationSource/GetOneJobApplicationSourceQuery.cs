using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetOneJobApplicationSource;

public class GetOneJobApplicationSourceQuery : IQuery<JobApplicationSourceResponseDTO>
{
    public GetOneJobApplicationSourceQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
