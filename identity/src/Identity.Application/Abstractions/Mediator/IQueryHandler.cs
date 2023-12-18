using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Abstractions.Mediator;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery: IQuery<TResponse>
{
    
}