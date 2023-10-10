using MlcAccounting.Referential.Domain.ProductAggregate.Abstractions;
using MlcAccounting.Referential.Domain.ProductAggregate.Entities;

namespace MlcAccounting.Referential.Infrastructure.Repositories;

public class ProductMongoRepository : IProductRepository
{
    public List<Product> Data { get; set; } = new();

    public Task<IEnumerable<Product>> GetAllAsync(string name) =>
        Task.FromResult(Data.Where(_ => _.Name == name));

    public Task<Product?> GetAsync(Guid id) =>
        Task.FromResult(Data.FirstOrDefault(_ => _.Id == id));

    public Task CreateAsync(Product product)
    {
        Data.Add(product);

        return Task.CompletedTask;
    }
}