using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели уточненной информации об одежде в модель базы данных
    /// </summary>
    public interface IClothesDetailEntityConverter : IEntityConverter<int, IClothesDetailDomain, ClothesEntity>
    { }
}