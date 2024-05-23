using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;

public class UpdateJobApplicationCommand : ICommand<Result<JobApplicationUpdatedResponseDTO>>
{
    public UpdateJobApplicationCommand(Guid id, Guid candidateId, Guid companyId, Guid jobApplicationSourceId)
    {
        Id = id;
        CandidateId = candidateId;
        CompanyId = companyId;
        JobApplicationSourceId = jobApplicationSourceId;
    }

    public Guid Id { get; set; }

    public Guid CandidateId { get; }

    public Guid CompanyId { get; }

    public Guid JobApplicationSourceId { get; }
}
