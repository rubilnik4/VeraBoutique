using BoutiqueCommon.Models.Implementations.Clothes;
using BoutiqueCommon.Models.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters
{
    /// <summary>
    /// Преобразования модели типа пола и модель базы данных
    /// </summary>
    public static class GenderEntityConverter
    {
        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public static IGender FromEntity(GenderEntity genderEntity) =>
            new Gender(genderEntity.GenderType, genderEntity.Name);

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public static GenderEntity ToEntity(IGender gender) =>
            new GenderEntity(gender.GenderType, gender.Name);
    }
}