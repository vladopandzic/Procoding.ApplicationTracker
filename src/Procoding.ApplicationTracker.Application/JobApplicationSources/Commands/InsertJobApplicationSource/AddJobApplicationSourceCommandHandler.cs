using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;

internal sealed class AddJobApplicationSourceCommandHandler : ICommandHandler<AddJobApplicationSourceCommand, JobApplicationSourceInsertedResponseDTO>
{
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddJobApplicationSourceCommandHandler(IJobApplicationSourceRepository jobApplicationSourceRepository, IUnitOfWork unitOfWork)
    {
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<JobApplicationSourceInsertedResponseDTO>> Handle(AddJobApplicationSourceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var jobApplicationSource = JobApplicationSource.Create(id, request.Name);

        await _jobApplicationSourceRepository.InsertAsync(jobApplicationSource, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var jobApplicationSourceDto = new JobApplicationSourceDTO(id, request.Name);

        return new JobApplicationSourceInsertedResponseDTO(jobApplicationSourceDto);
    }
}
