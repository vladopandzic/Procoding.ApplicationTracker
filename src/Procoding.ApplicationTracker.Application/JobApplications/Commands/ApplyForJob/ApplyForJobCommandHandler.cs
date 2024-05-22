using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;

internal class ApplyForJobCommandHandler : ICommandHandler<ApplyForJobCommand, JobApplicationInsertedResponseDTO>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplyForJobCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Result<JobApplicationInsertedResponseDTO>> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
