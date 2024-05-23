﻿using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;

public sealed class ApplyForJobCommand : ICommand<Result<JobApplicationInsertedResponseDTO>>
{
    public ApplyForJobCommand(Guid candidateId, Guid companyId, Guid jobApplicationSourceId)
    {
        CandidateId = candidateId;
        CompanyId = companyId;
        JobApplicationSourceId = jobApplicationSourceId;
    }

    public Guid CandidateId { get; }

    public Guid CompanyId { get; }

    public Guid JobApplicationSourceId { get; }

}
