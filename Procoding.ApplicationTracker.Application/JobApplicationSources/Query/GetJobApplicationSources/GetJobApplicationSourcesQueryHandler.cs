using MapsterMapper;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetJobApplicationSources;

internal sealed class GetJobApplicationSourcesQueryHandler : IQueryHandler<GetJobApplicationSourcesQuery, JobApplicationSourceListResponseDTO>
{
    private readonly IMapper _mapper;
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;

    public GetJobApplicationSourcesQueryHandler(IMapper mapper, IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        _mapper = mapper;
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
    }

    public async Task<JobApplicationSourceListResponseDTO> Handle(GetJobApplicationSourcesQuery request, CancellationToken cancellationToken)
    {
        var jobApplicationSources = await _jobApplicationSourceRepository.GetJobApplicationSourceAsync(cancellationToken);

        var jobApplicationSourcesDtos = _mapper.Map<List<JobApplicationSourceDTO>>(jobApplicationSources);

        return new JobApplicationSourceListResponseDTO(jobApplicationSourcesDtos.AsReadOnly());
    }
}
