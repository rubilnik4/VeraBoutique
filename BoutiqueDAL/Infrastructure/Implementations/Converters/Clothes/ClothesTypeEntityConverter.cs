using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeDomain, IClothesTypeEntity, ClothesTypeEntity>,
                                              IClothesTypeEntityConverter
    {
        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public override IClothesTypeDomain FromEntity(IClothesTypeEntity clothesTypeEntity) =>
            new ClothesTypeDomain(clothesTypeEntity.Name);

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeDomain clothesTypeDomain) =>
            new ClothesTypeEntity(clothesTypeDomain.Name);
    }
}