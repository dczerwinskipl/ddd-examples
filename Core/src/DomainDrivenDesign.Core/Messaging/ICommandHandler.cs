namespace DomainDrivenDesign.Core.Messaging;

public interface ICommandHandler<TCommand> where TCommand : Command
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

