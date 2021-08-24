using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели цвета одежды в модель базы данных
    /// </summary>
    public class ColorClothesEntityConverter : EntityConverter<string, IColorDomain, ColorEntity>,
                                               IColorClothesEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IColorDomain> FromEntity(ColorEntity colorEntity) =>
            new ColorDomain(colorEntity.Name).
            ToResultValue();

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ColorEntity ToEntity(IColorDomain colorDomain) =>
            new ColorEntity(colorDomain);
    }
}