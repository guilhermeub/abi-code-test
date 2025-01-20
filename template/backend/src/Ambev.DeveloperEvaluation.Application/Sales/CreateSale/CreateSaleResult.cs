namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Result of the CreateSaleCommand.
    /// </summary>
    public class CreateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the created sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Indicates whether the sale was successfully created.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// A message providing additional information about the result.
        /// </summary>
        public string Message { get; set; } = "Sale created successfully.";
    }
}
