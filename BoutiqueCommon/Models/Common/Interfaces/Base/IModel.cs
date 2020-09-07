namespace BoutiqueCommon.Models.Common.Interfaces.Base
{
    /// <summary>
    /// Базовая модель
    /// </summary>
    public interface IModel<TId> 
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