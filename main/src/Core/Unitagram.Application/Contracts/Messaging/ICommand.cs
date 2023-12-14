using MediatR;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Messaging;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}