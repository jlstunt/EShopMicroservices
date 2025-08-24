namespace Basket.API.Basket.DeleteBasket;

public record class DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record class DeleteBasketResult(bool IsSuccess);
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var savedBasket = await repository.DeleteBasket(command.UserName, cancellationToken);
        //TODO: update cache

        return new DeleteBasketResult(savedBasket);
    }
}
