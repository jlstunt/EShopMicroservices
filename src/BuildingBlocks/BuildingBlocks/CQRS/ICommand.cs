using MediatR;


namespace BuildingBlocks.CQRS;

//Generic command interface for CQRS pattern using MediatR
public interface ICommand : IRequest<Unit>
{
}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
