using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Base
{
    /// <summary>
    /// Базовая трансферная модель
    /// </summary>
    public interface ITransferModel<TId>: IModel<TId>
        where TId: notnull
    { }
}