using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы типов одежды
    /// </summary>
    public class ClothesTypeDatabaseValidateService : DatabaseValidateService<string, IClothesTypeMainDomain, ClothesTypeEntity>,
                                                      IClothesTypeDatabaseValidateService
    {
        public ClothesTypeDatabaseValidateService(IClothesTypeTable clothesTypeTable,
                                                  ICategoryDatabaseValidateService categoryDatabaseValidateService)
            : base(clothesTypeTable)
        {
            _categoryDatabaseValidateService = categoryDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        private readonly ICategoryDatabaseValidateService _categoryDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        protected override IResultError ValidateModel(IClothesTypeMainDomain clothesType) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothesType)).
            ResultErrorBindOk(() => ValidateCategoryName(clothesType));


        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesTypeMainDomain clothesTypeMain) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _categoryDatabaseValidateService.ValidateFind(clothesTypeMain.Category.Id));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesTypeMainDomain> clothesTypeMains) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesTypeMains.Select(clothesType => clothesType.Category.Id).
                                                          Distinct().
                                         Map(ids => _categoryDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(IClothesTypeDomain clothesType) =>
            clothesType.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<string, IClothesTypeDomain>(nameof(clothesType.Name), clothesType));

        /// <summary>
        /// Проверка наименования категории 
        /// </summary>
        private static IResultError ValidateCategoryName(IClothesTypeDomain clothesType) =>
            clothesType.CategoryName.ToResultValueWhere(
                categoryName => !String.IsNullOrWhiteSpace(categoryName),
                _ => ModelsErrors.FieldNotValid<string, IClothesTypeDomain>(nameof(clothesType.CategoryName), clothesType));
    }
}