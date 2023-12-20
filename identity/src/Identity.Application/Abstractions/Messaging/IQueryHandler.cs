using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery: Identity.Application.Abstractions.Messaging.IQuery<TResponse>
{
    
}