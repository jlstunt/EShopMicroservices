namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    // Configure the HTTP request pipeline.
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            //Use Mapster to map the incoming request to the "command" type MediatR expects
            var command = request.Adapt<CreateProductCommand>();

            //Send the command to the MediatR pipeline
            var result = await sender.Send(command);

            //Use Mapster to map the result to the response type MediatR expects
            var response = result.Adapt<CreateProductResponse>();

            //Return the response with a 201 Created status code and the location of the created resource
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Creates a new product in the catalog.");


    }

}
