
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductResult>
{
    // Handle the GetProductsQuery to retrieve products from the catalog
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        //Perform actual logic to retrieve products from the catalog
        logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult(products);
    }
}
