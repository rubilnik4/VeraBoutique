using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDALXUnit.Data.Models.Implementation
{
    /// <summary>
    /// Тестовая сущность для включения в запрос
    /// </summary>
    public class TestIncludeEntity : IEntityModel<string>
    {
        public TestIncludeEntity(string name)
            :this(name, null, null)
        { }

        public TestIncludeEntity(string name, TestEnum? testId, TestEntity? testEntity )
        {
            Name = name;
            TestId = testId;
            TestEntity = testEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name{ get; }

        /// <summary>
        /// Ключ к связующей сущности
        /// </summary>
        public TestEnum? TestId { get; set; }

        /// <summary>
        /// Тестовая связующая сущность
        /// </summary>
        public TestEntity? TestEntity { get; set; }
    }
}