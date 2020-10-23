using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели цвета одежды в модель базы данных
    /// </summary>
    public interface IColorClothesEntityConverter : IEntityConverter<string, IColorClothesDomain, IColorClothesEntity, ColorClothesEntity>
    { }
}