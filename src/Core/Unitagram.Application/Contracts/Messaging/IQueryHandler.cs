using MediatR;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery: IQuery<TResponse>
{
    
}