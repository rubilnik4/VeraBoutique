using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Base
{
    /// <summary>
    /// Базовая доменная модель
    /// </summary>
    public interface IDomainModel<out TId> : IModel<TId>
        where TId : notnull
    { }
}