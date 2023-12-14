using MediatR;
using Unitagram.Domain.Shared;

namespace Unitagram.Application.Contracts.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;