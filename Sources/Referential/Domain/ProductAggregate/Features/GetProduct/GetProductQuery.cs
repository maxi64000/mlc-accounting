using MediatR;
using MlcAccounting.Referential.Domain.ProductAggregate.Entities;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.GetProduct;

public class GetProductQuery : IRequest<Product?>
{
    public Guid Id { get; set; }
}