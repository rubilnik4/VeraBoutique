using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public class ClothesShortEntityConverter : EntityConverter<int, IClothesShortDomain, ClothesShortEntity>,
                                               IClothesShortEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IClothesShortDomain FromEntity(ClothesShortEntity clothesShortEntity) =>
            new ClothesShortDomain(clothesShortEntity.Id, clothesShortEntity.Name, clothesShortEntity.Price, clothesShortEntity.Image);

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesShortEntity ToEntity(IClothesShortDomain clothesShortDomain) =>
            new ClothesShortEntity(clothesShortDomain.Id, clothesShortDomain.Name, clothesShortDomain.Price, clothesShortDomain.Image);
    }
}