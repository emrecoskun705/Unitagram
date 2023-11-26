using MediatR;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand: ICommand
{
    
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand: ICommand<TResponse>
{
    
}