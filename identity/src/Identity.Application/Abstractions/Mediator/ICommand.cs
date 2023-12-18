using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Abstractions.Mediator;

public interface ICommand : IRequest<Result>
{
    
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}