using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestEntityConverter : EntityConverter<TestEnum, ITestDomain, TestEntity>, ITestEntityConverter
    {
        public TestEntityConverter(ITestIncludeEntityConverter testIncludeEntityConverter)
        {
            _testIncludeEntityConverter = testIncludeEntityConverter;
        }

        /// <summary>
        /// Преобразования вложенной доменной модели в модель базы данных
        /// </summary>
        private readonly ITestIncludeEntityConverter _testIncludeEntityConverter;

        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestDomain> FromEntity(TestEntity testEntity) =>
            GetTestFunc(testEntity).
            ResultCurryBindOk(_testIncludeEntityConverter.FromEntities(testEntity.TestIncludes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestEntity ToEntity(ITestDomain test) =>
            new TestEntity(test, _testIncludeEntityConverter.ToEntities(test.TestIncludes));

        /// <summary>
        /// Функция получения тестовых сущностей
        /// </summary>
        private static IResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>> GetTestFunc(ITestShortBase testShort) =>
            new ResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>>(
                testDomains => new TestDomain(testShort, testDomains));
    }
}