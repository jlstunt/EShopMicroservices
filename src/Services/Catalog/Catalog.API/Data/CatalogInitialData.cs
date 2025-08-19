using Marten.Schema;

namespace Catalog.API.Data;
public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        //Marten UPSERT to create records if they don't exist
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Samsung Galaxy S23 Ultra",
            Category = ["Smartphone", "Electronics"],
            Description = "The Samsung Galaxy S23 Ultra is a high-end smartphone with a 6.8-inch AMOLED display, Snapdragon 8 Gen 2 processor, and a versatile camera system.",
            ImageFile = "samsung-galaxy-s23-ultra.jpg",
            Price = 1199.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Apple MacBook Pro 16\"",
            Category = ["Laptop", "Electronics"],
            Description = "The Apple MacBook Pro 16\" features a stunning Retina display, M1 Pro chip, and up to 64GB of RAM for professional-grade performance.",
            ImageFile = "apple-macbook-pro-16.jpg",
            Price = 2499.99m
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Sony WH-1000XM5 Headphones",
            Category = ["Headphones", "Audio"],
            Description = "Experience industry-leading noise cancellation with the Sony WH-1000XM5 headphones, featuring touch controls and up to 30 hours of battery life.",
            ImageFile = "sony-wh-1000xm5.jpg",
            Price = 349.99m
        }
    };
}
