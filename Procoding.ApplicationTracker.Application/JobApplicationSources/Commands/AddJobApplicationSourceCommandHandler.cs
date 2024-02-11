using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands
{
    internal sealed class AddJobApplicationSourceCommandHandler : ICommandHandler<AddJobApplicationSourceCommand>
    {
        public Task Handle(AddJobApplicationSourceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
