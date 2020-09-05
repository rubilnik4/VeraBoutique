using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые данные сущностей
    /// </summary>
    public static class EntityData
    {
        /// <summary>
        /// Получить сущности типа пола
        /// </summary>
        public static List<GenderEntity> GetGenderEntities() =>
            new List<GenderEntity>()
            {
                new GenderEntity(GenderType.Male, "Мужик" ),
                new GenderEntity(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static List<Gender> GetGenders() =>
            new List<Gender>()
            {
                new Gender(GenderType.Male, "Мужик" ),
                new Gender(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static IReadOnlyCollection<GenderType> GetGendersIds() =>
            GetGenders().Select(gender => gender.GenderType).ToList().AsReadOnly();
    }
}