using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<UpdateSaleHandler> _logger;

        public UpdateSaleHandler(ISaleRepository saleRepository, ILogger<UpdateSaleHandler> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new UpdateSaleResult { IsSuccess = false, Message = $"Validation failed: {errors}" };
            }

            var sale = await _saleRepository.GetSaleByIdAsync(request.SaleId);
            if (sale == null)
            {
                return new UpdateSaleResult { IsSuccess = false, Message = "Sale not found" };
            }

            foreach (var item in request.Items)
            {
                if (item.Quantity > 20)
                {
                    return new UpdateSaleResult
                    {
                        IsSuccess = false,
                        Message = $"Cannot sell more than 20 units of {item.Product}."
                    };
                }

                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    item.Discount = 0.20m; 
                }
                else if (item.Quantity >= 4 && item.Quantity < 10)
                {
                    item.Discount = 0.10m; 
                }
                else
                {
                    item.Discount = 0.00m;
                }

                item.Price = item.Quantity * item.UnitPrice * (1 - item.Discount);
            }

            sale.Items = request.Items;
            sale.TotalSaleAmount = request.Items.Sum(i => i.Price);

            await _saleRepository.UpdateSaleAsync(sale);

            _logger.LogInformation($"Sale {sale.Id} updated successfully.");

            return new UpdateSaleResult
            {
                IsSuccess = true,
                Message = "Sale updated successfully",
                SaleNumber = sale.Id.ToString()
            };
        }
    }
}
