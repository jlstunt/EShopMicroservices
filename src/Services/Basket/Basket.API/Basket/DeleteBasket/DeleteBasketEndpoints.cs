namespace Basket.API.Basket.DeleteBasket;

//public record class DeleteBasketRequest(string UserName)
public record class DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketResult>();
            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete basket for user")
        .WithDescription("Deletes the shopping basket for a specific user by username.");
    }
}
