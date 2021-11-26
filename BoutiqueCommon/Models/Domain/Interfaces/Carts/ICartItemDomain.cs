using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Carts
{
    /// <summary>
    /// Позиция в корзине. Доменная модель
    /// </summary>
    public interface ICartItemDomain : ICartItemBase, IDomainModel<string>
    { }
}