using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;

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
        public static Gender FromEntity(GenderEntity genderEntity) =>
            new Gender(genderEntity.GenderType, genderEntity.Name);

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public static GenderEntity ToEntity(Gender gender) =>
            new GenderEntity(gender.GenderType, gender.Name);
    }
}