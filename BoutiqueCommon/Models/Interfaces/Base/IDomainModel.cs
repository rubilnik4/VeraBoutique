namespace BoutiqueCommon.Models.Interfaces.Base
{
    /// <summary>
    /// Базовая доменная модель
    /// </summary>
    public interface IDomainModel<TId> 
        where TId: notnull
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        TId Id{ get; }

        /// <summary>
        /// Содержит ли идентификатор
        /// </summary>
        bool HasId(TId id);
    }
}