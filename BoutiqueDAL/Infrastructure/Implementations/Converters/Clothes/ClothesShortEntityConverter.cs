using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntity;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public class ClothesShortEntityConverter : EntityConverter<int, IClothesShortDomain, IClothesShortEntity, ClothesShortEntity>,
                                               IClothesShortEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesShortDomain> FromEntity(IClothesShortEntity clothesShortEntity) =>
            new ClothesShortDomain(clothesShortEntity.Id, clothesShortEntity.Name, clothesShortEntity.Price, clothesShortEntity.Image).
            Map(clothesShort => new ResultValue<IClothesShortDomain>(clothesShort));

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesShortEntity ToEntity(IClothesShortDomain clothesShortDomain) =>
            new ClothesShortEntity(clothesShortDomain.Id, clothesShortDomain.Name, clothesShortDomain.Price, clothesShortDomain.Image);
    }
}