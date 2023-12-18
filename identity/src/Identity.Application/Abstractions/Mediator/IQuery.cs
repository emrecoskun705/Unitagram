using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Abstractions.Mediator;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;