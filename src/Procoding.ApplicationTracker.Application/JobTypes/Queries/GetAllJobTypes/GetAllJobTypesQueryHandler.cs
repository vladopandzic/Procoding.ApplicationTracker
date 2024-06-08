using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;

namespace Procoding.ApplicationTracker.Application.JobTypes.Queries.GetAllJobTypes;


internal sealed class GetAllJobTypesQueryHandler : IQueryHandler<GetAllJobTypesQuery, JobTypeListResponseDTO>
{
    private readonly IJobTypeRepository _jobTypeRepository;

    public GetAllJobTypesQueryHandler(IJobTypeRepository jobTypeRepository)
    {
        _jobTypeRepository = jobTypeRepository;
    }

    public async Task<JobTypeListResponseDTO> Handle(GetAllJobTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _jobTypeRepository.GetAllJobTypesAsync(cancellationToken);

        var jobTypes = result.Select(x => new JobTypeDTO(x.Value)).ToList();

        return new JobTypeListResponseDTO(jobTypes, jobTypes.Count());
    }
}
