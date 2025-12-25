namespace Ordering.Domain.Value_Objects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;
    
    public static ProductId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ProductId cannot be empty.");
        }
        
        return new ProductId(value);
    }
}
