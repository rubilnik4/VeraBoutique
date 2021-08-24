using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ImageEntities
{
    public class ClothesImageEntityConverter : EntityConverter<int, IClothesImageDomain, ClothesImageEntity>,
                                               IClothesImageEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesImageDomain> FromEntity(ClothesImageEntity clothesImageEntity) =>
            new ClothesImageDomain(clothesImageEntity).
            ToResultValue();

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesImageEntity ToEntity(IClothesImageDomain clothesImageDomain) =>
            new ClothesImageEntity(clothesImageDomain);
    }
}