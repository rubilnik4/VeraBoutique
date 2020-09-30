using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDALXUnit.Data.Models.Implementation
{
    /// <summary>
    /// Тестовая сущность базы данных
    /// </summary>
    public class TestEntity : Test, IEntityModel<TestEnum>
    {
        public TestEntity(TestEnum testEnum, string name)
            : this(testEnum, name, Enumerable.Empty<TestIncludeEntity> ())
        { }

        public TestEntity(TestEnum testEnum, string name, IEnumerable<TestIncludeEntity> testIncludeEntities)
            : base(testEnum, name)
        {
            TestIncludeEntities = testIncludeEntities.ToList();
        }

        /// <summary>
        /// Тестовые связующие сущности
        /// </summary>
        public IReadOnlyCollection<TestIncludeEntity> TestIncludeEntities { get; set; }
    }
}