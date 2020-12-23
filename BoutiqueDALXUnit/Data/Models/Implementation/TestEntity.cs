using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDALXUnit.Data.Models.Interfaces;


namespace BoutiqueDALXUnit.Data.Models.Implementation
{
    /// <summary>
    /// Тестовая сущность базы данных
    /// </summary>
    public class TestEntity : TestBase<TestIncludeEntity>, ITestEntity
    {
        public TestEntity(ITestShortBase testShort, IEnumerable<TestIncludeEntity> testIncludeEntities)
          : this(testShort.TestEnum, testShort.Name, testIncludeEntities)
        { }

        public TestEntity(TestEnum testEnum, string name, IEnumerable<TestIncludeEntity> testIncludeEntities)
            : base(testEnum, name, testIncludeEntities)
        { }
    }
}