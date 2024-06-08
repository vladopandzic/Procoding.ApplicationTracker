using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;

public sealed class ApplyForJobCommand : ICommand<Result<JobApplicationInsertedResponseDTO>>
{
    public ApplyForJobCommand(Guid candidateId,
                              Guid companyId,
                              Guid jobApplicationSourceId,
                              string jobPositionTitle,
                              string jobAdLink,
                              string jobType,
                              string workLocationType,
                              string? description)
    {
        CandidateId = candidateId;
        CompanyId = companyId;
        JobApplicationSourceId = jobApplicationSourceId;
        JobPositionTitle = jobPositionTitle;
        JobAdLink = jobAdLink;
        JobType = jobType;
        WorkLocationType = workLocationType;
        Description = description;
    }

    public Guid CandidateId { get; }

    public Guid CompanyId { get; }

    public Guid JobApplicationSourceId { get; }

    public string JobPositionTitle { get; set; }

    public string JobAdLink { get; set; }

    public string JobType { get; set; }

    public string WorkLocationType { get; set; }

    public string? Description { get; set; }
}
