
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    async Task<GetProductByCategoryResult> IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>.Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        // Retrieve the product from the database using the provided category
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
