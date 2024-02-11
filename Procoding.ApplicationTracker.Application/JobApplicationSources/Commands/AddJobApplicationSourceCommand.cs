using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands
{
    public sealed class AddJobApplicationSourceCommand : ICommand
    {
        /// <summary>
        /// Name of the application source
        /// </summary>
        public string Name { get; } = default!;
    }
}
