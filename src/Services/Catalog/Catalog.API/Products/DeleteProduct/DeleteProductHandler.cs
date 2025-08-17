
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required.");
    }
}
internal class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
         //var origProduct = await session.LoadAsync<Product>(command.Id, cancellationToken)
        var productExists = await session.Query<Product>().AnyAsync(p => p.Id == command.Id, cancellationToken);

        if(!productExists)
        {
            throw new ProductNotFoundException(command.Id);
        }

        // Delete the product
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        // Check if the product was found and deleted
        productExists = await session.Query<Product>().AnyAsync(p => p.Id == command.Id, cancellationToken);
        if (productExists)
        {
            // If the product still exists, it means deletion failed
            return new DeleteProductResult(false);
        }
        // Return success status
        return new DeleteProductResult(true);
    }
}
