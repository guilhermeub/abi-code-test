using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem
{
    public class CancelItemCommand : IRequest<CancelItemResponse>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }

        public CancelItemCommand(Guid id)
        {
            ItemId = id;
        }
    }
}
