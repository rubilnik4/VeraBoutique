using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts
{
    /// <summary>
    /// Преобразования модели позиции в корзине в модель базы данных
    /// </summary>
    public interface ICartItemEntityConverter : IEntityConverter<string, ICartItemDomain, CartItemEntity>
    { }
}