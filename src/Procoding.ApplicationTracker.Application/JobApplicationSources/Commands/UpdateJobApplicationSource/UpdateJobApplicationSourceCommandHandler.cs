using FluentValidation;
using LanguageExt.Common;
using MapsterMapper;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;

internal sealed class UpdateJobApplicationSourceCommandHandler : ICommandHandler<UpdateJobApplicationSourceCommand, Result<JobApplicationSourceUpdatedResponseDTO>>
{
    private readonly IMapper _mapper;
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateJobApplicationSourceCommandHandler(IMapper mapper, IJobApplicationSourceRepository jobApplicationSourceRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<JobApplicationSourceUpdatedResponseDTO>> Handle(UpdateJobApplicationSourceCommand request, CancellationToken cancellationToken)
    {
        var jobApplicationSource = await _jobApplicationSourceRepository.GetJobApplicationSourceAsync(request.Id, cancellationToken);

        //TODO: use result object
        if (jobApplicationSource is null)
        {
            var error = new ValidationException("Job application source does not exist");

            return new Result<JobApplicationSourceUpdatedResponseDTO>(error);
        }

        jobApplicationSource.ChangeName(request.Name);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var jobApplicationSourceDto = new JobApplicationSourceDTO(request.Id, request.Name);

        return new JobApplicationSourceUpdatedResponseDTO(jobApplicationSourceDto);
    }
}
