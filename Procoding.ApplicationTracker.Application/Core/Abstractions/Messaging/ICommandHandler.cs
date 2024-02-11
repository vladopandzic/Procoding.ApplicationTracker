using MediatR;

namespace Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging
{
    /// <summary>
    /// Represents the command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">The command type.</typeparam>
    /// <typeparam name="TResponse">The command response type.</typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
       where TCommand : ICommand
    {
    }
}
