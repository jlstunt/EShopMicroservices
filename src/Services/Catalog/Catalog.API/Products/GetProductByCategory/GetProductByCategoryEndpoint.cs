
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryQuery()
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {            
            // Send the query to the MediatR pipeline
            var result = await sender.Send(new GetProductByCategoryQuery(category));

            // Map the result to the response type
            var response = result.Adapt<GetProductByCategoryResponse>();

            // Return the response with a 200 OK status code
            return Results.Ok(response);
        }).WithName("GetProductByCategory")
          .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Products by Category")
          .WithDescription("Retrieves products from the catalog by category.");
    }
}
