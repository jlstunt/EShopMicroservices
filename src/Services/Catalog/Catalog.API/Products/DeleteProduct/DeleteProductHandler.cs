
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        // Log the command handling
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

        //var origProduct = await session.LoadAsync<Product>(command.Id, cancellationToken)
        var productExists = await session.Query<Product>().AnyAsync(p => p.Id == command.Id, cancellationToken);

        if(!productExists)
        {
            logger.LogInformation("Product with Id {ProductId} not found", command.Id);
            throw new ProductNotFoundException(command.Id);
        }

        // Delete the product
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        // Check if the product was found and deleted
        productExists = await session.Query<Product>().AnyAsync(p => p.Id == command.Id, cancellationToken);
        if (productExists)
        {
            logger.LogInformation("Product Id with Id: {#@Id} still exists. Delete Failed.", command.Id);
            // If the product still exists, it means deletion failed
            return new DeleteProductResult(false);
        }
        // Return success status
        return new DeleteProductResult(true);
    }
}
