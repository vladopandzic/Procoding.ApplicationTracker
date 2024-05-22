using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetJobApplicationSources;

internal sealed class GetJobApplicationSourcesQueryHandler : IQueryHandler<GetJobApplicationSourcesQuery, JobApplicationSourceListResponseDTO>
{
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;

    public GetJobApplicationSourcesQueryHandler(IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
    }

    public async Task<JobApplicationSourceListResponseDTO> Handle(GetJobApplicationSourcesQuery request, CancellationToken cancellationToken)
    {
        var jobApplicationSources = await _jobApplicationSourceRepository.GetJobApplicationSourceAsync(cancellationToken);

        var jobApplicationSourcesDtos = jobApplicationSources.Select(x => new JobApplicationSourceDTO(x.Id, x.Name));

        return new JobApplicationSourceListResponseDTO(jobApplicationSourcesDtos.ToList().AsReadOnly());
    }
}
