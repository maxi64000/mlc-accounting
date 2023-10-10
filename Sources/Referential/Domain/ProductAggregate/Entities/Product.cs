namespace MlcAccounting.Referential.Domain.ProductAggregate.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Product(string name)
    {
        Name = name;
    }
}