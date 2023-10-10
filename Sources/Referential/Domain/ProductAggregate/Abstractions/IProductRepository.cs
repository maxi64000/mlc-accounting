using MlcAccounting.Referential.Domain.ProductAggregate.Entities;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Abstractions;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(string name);

    Task<Product?> GetAsync(Guid id);

    Task CreateAsync(Product product);
}