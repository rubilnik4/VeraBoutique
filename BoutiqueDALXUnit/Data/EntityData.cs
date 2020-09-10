using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database;

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
        /// Получить сущности для теста
        /// </summary>
        public static List<TestEntity> GetTestEntity() =>
            new List<TestEntity>()
            {
                new TestEntity(TestEnum.First, "First" ),
                new TestEntity(TestEnum.Second, "Second"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static List<IGenderDomain> GetGenders() =>
            new List<IGenderDomain>()
            {
                new GenderDomain(GenderType.Male, "Мужик" ),
                new GenderDomain(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static IReadOnlyCollection<GenderType> GetGendersIds() =>
            GetGenders().Select(gender => gender.GenderType).ToList().AsReadOnly();
    }
}