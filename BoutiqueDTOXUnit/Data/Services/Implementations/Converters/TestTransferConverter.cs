using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTOXUnit.Data.Services.Implementations.Converters
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
            ResultValueCurryOk(_testIncludesTransferConverter.GetDomains(testTransfer.TestIncludes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestTransfer ToTransfer(ITestDomain test) =>
            new (test, _testIncludesTransferConverter.ToTransfers(test.TestIncludes));

        /// <summary>
        /// Функция получения группы размеров
        /// </summary>
        private static IResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>> GetTestFunc(ITestShortBase testShort) =>
            new ResultValue<Func<IEnumerable<ITestIncludeDomain>, ITestDomain>>(
                testIncludes => new TestDomain(testShort, testIncludes));
    }
}