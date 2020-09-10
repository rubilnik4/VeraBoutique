using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;

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
            GetGendersDomain().
            Select(genderDomain => new GenderEntity(genderDomain.GenderType, genderDomain.Name)).
            ToList();

        /// <summary>
        /// Получить сущности для теста
        /// </summary>
        public static List<TestEntity> GetTestEntity() =>
            GetTestDomains().
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name)).
            ToList();

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static List<IGenderDomain> GetGendersDomain() =>
            new List<IGenderDomain>()
            {
                new GenderDomain(GenderType.Male, "Мужик" ),
                new GenderDomain(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Получить тестовые модели
        /// </summary>
        public static List<ITestDomain> GetTestDomains() =>
            new List<ITestDomain>()
            {
                new TestDomain(TestEnum.First, "First" ),
                new TestDomain(TestEnum.Second, "Second"),
            };

        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static IReadOnlyCollection<GenderType> GetGendersIds() =>
            GetGendersDomain().Select(gender => gender.GenderType).ToList().AsReadOnly();
    }
}