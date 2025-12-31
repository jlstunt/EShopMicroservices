namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDBContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //Create Order
        var order = CreateNewOrder(command.Order);

        //Save to db
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        //Return result
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FristName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.Addressline, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(orderDto.BillingAddress.FristName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.Addressline, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        var newOrder = Order.Create(
        id: OrderId.Of(Guid.NewGuid()),
        customerId: CustomerId.Of(orderDto.CustomerId),
        orderName: OrderName.Of(orderDto.OrderName),
        shippingAddress: shippingAddress,
        billingAddress: billingAddress,
        payment: payment
        );

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(
                ProductId.Of(orderItemDto.Productid),
                orderItemDto.Quantity,
                orderItemDto.Price);
        }

        return newOrder;
    }
}
