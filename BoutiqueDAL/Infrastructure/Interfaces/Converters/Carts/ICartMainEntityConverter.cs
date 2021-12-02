using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Carts;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts
{
    /// <summary>
    /// Преобразования модели корзины в модель базы данных
    /// </summary>
    public interface ICartMainEntityConverter : IEntityConverter<string, ICartMainDomain, CartEntity>
    { }
}