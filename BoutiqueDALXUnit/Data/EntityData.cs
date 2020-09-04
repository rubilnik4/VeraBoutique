using System.Collections.Generic;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;

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
                new GenderEntity(GenderType.Femalele, "Тетя"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static List<Gender> GetGenders() =>
            new List<Gender>()
            {
                new Gender(GenderType.Male, "Мужик" ),
                new Gender(GenderType.Femalele, "Тетя"),
            };
    }
}