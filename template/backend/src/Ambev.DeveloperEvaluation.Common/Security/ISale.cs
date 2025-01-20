namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        ///The identifier of the sale.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The number of the sale.
        /// </summary>
        public string SaleNumber { get; }
    }
}
