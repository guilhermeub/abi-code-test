namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Result of the CancelSaleCommand.
    /// </summary>
    public class CancelSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cancelled sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Indicates whether the sale cancellation was successful.
        /// </summary>
        public bool IsCancelled { get; set; } = true;

        /// <summary>
        /// A message providing additional information about the result.
        /// </summary>
        public string Message { get; set; } = "Sale cancelled successfully.";
    }
}
