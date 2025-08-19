
namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            // Create a query to get products
            var query = request.Adapt<GetProductsQuery>();

            // Send the query to the MediatR pipeline
            var result = await sender.Send(query);

            // Map the result to the response type
            var response = result.Adapt<GetProductResponse>();

            // Return the response with a 200 OK status code
            return Results.Ok(response);
        }).WithName("GetProducts")
          .Produces<GetProductResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Products")
          .WithDescription("Retrieves all products from the catalog.");
    }
}
