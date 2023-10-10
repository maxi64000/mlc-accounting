using MediatR;
using MlcAccounting.Referential.Domain.ProductAggregate.Abstractions;
using MlcAccounting.Referential.Domain.ProductAggregate.Entities;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly IProductRepository _repository;

    public GetProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken) =>
        await _repository.GetAsync(request.Id);
}