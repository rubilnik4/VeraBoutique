using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate
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
        public override IResultError ValidateModel(IClothesTypeMainDomain clothesType) =>
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
                _ => DatabaseFieldErrors.FieldNotValid(clothesType.Name, nameof(IClothesTypeTable)));

        /// <summary>
        /// Проверка наименования категории 
        /// </summary>
        private static IResultError ValidateCategoryName(IClothesTypeDomain clothesType) =>
            clothesType.CategoryName.ToResultValueWhere(
                categoryName => !String.IsNullOrWhiteSpace(categoryName),
                _ => DatabaseFieldErrors.FieldNotValid(clothesType.CategoryName, nameof(IClothesTypeTable)));
    }
}