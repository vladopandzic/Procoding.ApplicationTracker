using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;

namespace Procoding.ApplicationTracker.Application.JobTypes.Queries.GetAllJobTypes;

public sealed class GetAllJobTypesQuery : IQuery<JobTypeListResponseDTO>
{
}
