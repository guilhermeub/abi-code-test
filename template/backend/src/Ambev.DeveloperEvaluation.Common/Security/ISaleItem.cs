namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface ISaleItem
    {
        /// <summary>
        /// Obtém o identificador único do usuário.
        /// </summary>
        /// <returns>O ID do usuário como uma string.</returns>
        public Guid Id { get; }
    }
}
