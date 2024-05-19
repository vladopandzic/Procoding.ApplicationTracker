using MapsterMapper;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;

internal sealed class AddJobApplicationSourceCommandHandler : ICommandHandler<AddJobApplicationSourceCommand, JobApplicationSourceInsertedResponseDTO>
{
    private readonly IMapper _mapper;
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddJobApplicationSourceCommandHandler(IMapper mapper, IJobApplicationSourceRepository jobApplicationSourceRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<JobApplicationSourceInsertedResponseDTO> Handle(AddJobApplicationSourceCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var jobApplicationSource = JobApplicationSource.Create(id, request.Name);

        await _jobApplicationSourceRepository.InsertAsync(jobApplicationSource, cancellationToken);

        var a = await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var jobApplicationSourceDto = new JobApplicationSourceDTO(id, request.Name);

        return new JobApplicationSourceInsertedResponseDTO(jobApplicationSourceDto);
    }
}
