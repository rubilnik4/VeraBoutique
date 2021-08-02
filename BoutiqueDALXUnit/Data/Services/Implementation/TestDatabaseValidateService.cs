using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Тестовый сервис проверки данных
    /// </summary>
    public class TestDatabaseValidateService : DatabaseValidateService<TestEnum, ITestDomain, TestEntity>,
                                               ITestDatabaseValidateService
    {
        public TestDatabaseValidateService(ITestTable testTable, 
                                           ITestIncludeDatabaseValidateService testIncludeDatabaseValidateService)
            : base(testTable)
        {
            _testIncludeDatabaseValidateService = testIncludeDatabaseValidateService;
        }

        /// <summary>
        /// Тестовый сервис проверки вложенных данных
        /// </summary>
        private readonly ITestIncludeDatabaseValidateService _testIncludeDatabaseValidateService;
        
        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(ITestDomain test) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(test));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(ITestDomain test) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => test.TestIncludes.Select(testInclude => testInclude.Id).
                                         Map(ids => _testIncludeDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<ITestDomain> tests) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => tests.SelectMany(test => test.TestIncludes.Select(testInclude => testInclude.Id)).
                                               Distinct().
                                         Map(ids => _testIncludeDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(ITestDomain test) =>
            test.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<TestEnum, ITestDomain>(nameof(test.Name), test));
    }
}