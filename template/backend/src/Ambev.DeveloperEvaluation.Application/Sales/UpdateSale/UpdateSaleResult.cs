namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Result of the UpdateSaleCommand.
    /// </summary>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the updated sale.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Indicates whether the sale update was successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// A message providing additional information about the result.
        /// </summary>
        public string Message { get; set; } = "Sale updated successfully.";
    }
}
