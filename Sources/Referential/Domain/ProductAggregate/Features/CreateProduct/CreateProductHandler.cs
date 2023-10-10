using MediatR;
using MlcAccounting.Referential.Domain.ProductAggregate.Abstractions;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid?>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAllAsync(request.Name);

        if (result.Any())
        {
            return null;
        }

        var product = request.ToEntity();

        await _repository.CreateAsync(product);

        return product.Id;
    }
}