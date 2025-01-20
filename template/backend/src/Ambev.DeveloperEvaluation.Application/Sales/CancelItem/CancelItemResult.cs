namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItem
{
    /// <summary>
    /// Result of the CancelItemCommand.
    /// </summary>
    public class CancelItemResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cancelled item.
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Indicates whether the item cancellation was successful.
        /// </summary>
        public bool IsCancelled { get; set; } = true;

        /// <summary>
        /// A message providing additional information about the result.
        /// </summary>
        public string Message { get; set; } = "Item cancelled successfully.";
    }
}
