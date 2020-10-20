using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели цвета одежды в модель базы данных
    /// </summary>
    public class ColorClothesEntityConverter : EntityConverter<string, IColorClothesDomain, ColorClothesEntity>,
                                               IColorClothesEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IColorClothesDomain FromEntity(ColorClothesEntity colorClothesEntity) =>
            new ColorClothesDomain(colorClothesEntity.Name);

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ColorClothesEntity ToEntity(IColorClothesDomain colorClothesDomain) =>
            new ColorClothesEntity(colorClothesDomain.Name);
    }
}