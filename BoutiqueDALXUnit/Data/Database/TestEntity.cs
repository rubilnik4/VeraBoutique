using System;
using System.Diagnostics.CodeAnalysis;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDALXUnit.Data.Database
{
    /// <summary>
    /// Тип пола для тестов
    /// </summary>
    public class TestEntity : IEntityModel<TestEnum>, IEquatable<TestEntity>
    {
        public TestEntity(TestEnum testEnum, string name)
        {
            TestEnum = testEnum;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TestEnum Id => TestEnum;

        /// <summary>
        /// Тестовое перечисление
        /// </summary>
        public TestEnum TestEnum { get; }

        /// <summary>
        /// Тестовое поле
        /// </summary>
        public string Name { get; set; }

        #region IEquatable
        public override bool Equals(object obj) => obj is TestEntity testEntity && Equals(testEntity);

        public bool Equals(TestEntity other) =>
            other?.TestEnum == TestEnum &&
            other.Name == Name;

        public override int GetHashCode() => HashCode.Combine(TestEnum, Name);
        #endregion
    }
}