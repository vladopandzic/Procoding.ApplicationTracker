using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;

public sealed class UpdateJobApplicationCommand : ICommand<Result<JobApplicationUpdatedResponseDTO>>
{
    public UpdateJobApplicationCommand(Guid id,
                                       Guid candidateId,
                                       Guid companyId,
                                       Guid jobApplicationSourceId,
                                       string jobPositionTitle,
                                       string workLocationType,
                                       string jobType,
                                       string jobAdLink,
                                       string? description)
    {
        Id = id;
        CandidateId = candidateId;
        CompanyId = companyId;
        JobApplicationSourceId = jobApplicationSourceId;
        JobPositionTitle = jobPositionTitle;
        WorkLocationType = workLocationType;
        JobType = jobType;
        JobAdLink = jobAdLink;
        Description = description;
    }

    public Guid Id { get; set; }

    public Guid CandidateId { get; }

    public Guid CompanyId { get; }

    public Guid JobApplicationSourceId { get; }

    public string? Description { get; }

    public string JobPositionTitle { get; }

    public string WorkLocationType { get; }

    public string JobType { get; }

    public string JobAdLink { get; }
}
