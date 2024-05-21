using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;

public sealed class AddJobApplicationSourceCommand : ICommand<Result<JobApplicationSourceInsertedResponseDTO>>
{
    public AddJobApplicationSourceCommand(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Name of the job application source
    /// </summary>
    public string Name { get; } = default!;
}
