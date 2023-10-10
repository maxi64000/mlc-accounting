using MlcAccounting.Referential.Domain.ProductAggregate.Entities;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.CreateProduct;

public static class CreateProductMapper
{
    public static Product ToEntity(this CreateProductCommand command) => new(command.Name);
}