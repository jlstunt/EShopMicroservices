namespace Ordering.Domain.Value_Objects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;
    
    public static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new ArgumentException("OrderItemId cannot be empty ");
        }

        return new OrderItemId(value);
    }
}
