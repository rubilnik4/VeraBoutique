namespace BoutiqueCommon.Models.Interfaces.Base
{
    /// <summary>
    /// Базовая доменная модель
    /// </summary>
    public interface IDomainModel<out TId>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TId Id{ get; }
    }
}