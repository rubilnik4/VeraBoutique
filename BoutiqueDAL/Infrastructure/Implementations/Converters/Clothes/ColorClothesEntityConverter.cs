using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели цвета одежды в модель базы данных
    /// </summary>
    public class ColorClothesEntityConverter : EntityConverter<string, IColorDomain, ColorClothesEntity>,
                                               IColorClothesEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IColorDomain> FromEntity(ColorClothesEntity colorClothesEntity) =>
            new ColorDomain(colorClothesEntity.Name).
            Map(category => new ResultValue<IColorDomain>(category));

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ColorClothesEntity ToEntity(IColorDomain colorDomain) =>
            new ColorClothesEntity(colorDomain.Name);
    }
}