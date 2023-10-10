using MediatR;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.CreateProduct;

public class CreateProductCommand : IRequest<Guid?>
{
    public string Name { get; set; }

    public CreateProductCommand(string name)
    {
        Name = name;
    }
}