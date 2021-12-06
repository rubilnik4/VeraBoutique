using System;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Carts
{
    /// <summary>
    /// Корзина. Трансферная модель
    /// </summary>
    public interface ICartTransfer: ICartBase, ITransferModel<Guid>
    { }
}