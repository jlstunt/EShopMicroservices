namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string Username)
public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(UserName));
            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get basket for user")
        .WithDescription("Gets the shopping basket for a specific user by username.");
    }
}
