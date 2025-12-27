namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), "John Doe", "jllee@crimson.ua.edu"),
        Customer.Create(CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), "Jane Smith", "sailingbyashbreeze@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("33333333-3333-3333-3333-333333333333")), "Alice Johnson", "Jenny.lee@outlook.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), "IPhone X", 500),
        Product.Create(ProductId.Of(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), "Samsung 10", 400),
        Product.Create(ProductId.Of(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc")), "Huwaei Plus", 650),
        Product.Create(ProductId.Of(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")), "Xiaomi Mi", 450)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Jennifer", "Lee", "jenny.l.lee@outlook.com", "1585 Poplar Oaks Circle", "USA", "TN", "38017");
            var address2 = Address.Of("Travis", "Rogers", "trogers07111984@gmail.com", "275 Carolton Cove", "USA", "TN", "38017");

            var payment1 = Payment.Of("Visa","4111111111111111", "12/29", "123",1);
            var payment2 = Payment.Of("Discover", "4111111111111111", "12/26", "456", 1);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")),
                OrderName.Of("Ord_1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);
            order1.Add(ProductId.Of(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 2, 500);
            order1.Add(ProductId.Of(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 1, 400);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")),
                OrderName.Of("Ord_2"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2);
            order2.Add(ProductId.Of(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc")), 1, 650);
            order2.Add(ProductId.Of(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd")), 3, 450);
        
            return new List<Order> { order1,order2 };
        }
    }
}
