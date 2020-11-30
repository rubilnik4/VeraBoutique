using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTOXUnit.Data.Services.Implementations
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestTransferConverter : TransferConverter<TestEnum, ITestDomain, TestTransfer>, ITestTransferConverter
    {
        public TestTransferConverter(ITestIncludesTransferConverter testIncludesTransferConverter)
        {
            _testIncludesTransferConverter = testIncludesTransferConverter;
        }

        /// <summary>
        /// Тестовый конвертер включенных трансферных моделей
        /// </summary>
        private readonly ITestIncludesTransferConverter _testIncludesTransferConverter;

        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestDomain> FromTransfer(TestTransfer testTransfer) =>
            GetTestFunc(testTransfer).
            ResultCurryOkBind(_testIncludesTransferConverter.GetDomains(testTransfer.TestIncludes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestTransfer ToTransfer(ITestDomain test) =>
            new TestTransfer(test, _testIncludesTransferConverter.ToTransfers(test.TestIncludes));

        /// <summary>
        /// Функция получения группы размеров
        /// </summary>
        private static IResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>> GetTestFunc(ITest test) =>
            new ResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>>(
                testIncludes => new TestDomain(test, testIncludes));
    }
}