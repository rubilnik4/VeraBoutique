using System;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Carts
{
    /// <summary>
    /// Корзина. Доменная модель
    /// </summary>
    public interface ICartDomain : ICartBase, IDomainModel<Guid>
    { }
}