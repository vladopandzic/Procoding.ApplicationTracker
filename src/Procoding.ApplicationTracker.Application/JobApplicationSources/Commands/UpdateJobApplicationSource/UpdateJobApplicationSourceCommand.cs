﻿using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;

public sealed class UpdateJobApplicationSourceCommand : ICommand<Result<JobApplicationSourceUpdatedResponseDTO>>
{
    public UpdateJobApplicationSourceCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Id of the job application source
    /// </summary>
    public Guid Id { get; } = default!;

    /// <summary>
    /// Name of the job application source
    /// </summary>
    public string Name { get; } = default!;
}
