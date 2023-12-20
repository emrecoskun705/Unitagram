using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;