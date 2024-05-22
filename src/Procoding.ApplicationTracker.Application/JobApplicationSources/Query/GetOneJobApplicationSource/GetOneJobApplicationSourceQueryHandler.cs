using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetOneJobApplicationSource;

internal sealed class GetOneJobApplicationSourceQueryHandler : IQueryHandler<GetOneJobApplicationSourceQuery, JobApplicationSourceResponseDTO>
{
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;

    public GetOneJobApplicationSourceQueryHandler(IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
    }

    public async Task<JobApplicationSourceResponseDTO> Handle(GetOneJobApplicationSourceQuery request, CancellationToken cancellationToken)
    {
        var jobApplicationSource = await _jobApplicationSourceRepository.GetJobApplicationSourceAsync(request.Id, cancellationToken);

        //TODO: result object?
        if (jobApplicationSource is null)
            throw new Domain.Exceptions.JobApplicationSourceDoesNotExistException();

        var jobApplicationSourceDto = new JobApplicationSourceDTO(jobApplicationSource.Id, jobApplicationSource.Name);

        return new JobApplicationSourceResponseDTO(jobApplicationSourceDto);
    }
}
