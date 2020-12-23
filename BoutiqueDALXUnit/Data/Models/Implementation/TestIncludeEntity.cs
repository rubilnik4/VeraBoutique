using System;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDALXUnit.Data.Models.Implementation
{
    /// <summary>
    /// Тестовая сущность для включения в запрос
    /// </summary>
    public class TestIncludeEntity : TestIncludeBase, IEntityModel<string>
    {
        public TestIncludeEntity(ITestIncludeBase testInclude)
           : this(testInclude.Name)
        { }

        public TestIncludeEntity(string name)
            : this(name, null)
        { }

        public TestIncludeEntity(string name, TestEntity? testEntity)
            : base(name)
        {
            TestId = testEntity?.Id;
            TestEntity = testEntity;
        }

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