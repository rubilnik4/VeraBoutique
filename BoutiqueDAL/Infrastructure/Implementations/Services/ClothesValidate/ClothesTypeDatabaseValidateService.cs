using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
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
    public class ClothesTypeDatabaseValidateService : DatabaseValidateService<string, IClothesTypeDomain, ClothesTypeEntity>,
                                                      IClothesTypeDatabaseValidateService
    {
        public ClothesTypeDatabaseValidateService(IClothesTypeTable clothesTypeTable,
                                                  ICategoryDatabaseValidateService categoryDatabaseValidateService,
                                                  IGenderDatabaseValidateService genderDatabaseValidateService)
            : base(clothesTypeTable)
        {
            _categoryDatabaseValidateService = categoryDatabaseValidateService;
            _genderDatabaseValidateService = genderDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        private readonly ICategoryDatabaseValidateService _categoryDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private readonly IGenderDatabaseValidateService _genderDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        protected override IResultError ValidateModel(IClothesTypeDomain clothesType) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothesType)).
            ResultErrorBindOk(() => ValidateCategoryName(clothesType)).
            ResultErrorBindOk(() => ValidateGenders(clothesType));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesTypeDomain clothesType) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _categoryDatabaseValidateService.ValidateFind(clothesType.Category.Id)).
            ResultErrorBindOkBindAsync(() => clothesType.Genders.Select(gender => gender.Id).
                                             Map(ids => _genderDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesTypeDomain> clothesTypes) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesTypes.Select(clothesType => clothesType.Category.Id).
                                                      Distinct().
                                         Map(ids => _categoryDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesTypes.SelectMany(clothesType => clothesType.Genders.Select(gender => gender.Id)).
                                                          Distinct().
                                             Map(ids => _genderDatabaseValidateService.ValidateFinds(ids)));

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

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateGenders(IClothesTypeDomain clothesType) =>
            _genderDatabaseValidateService.ValidateQuantity(clothesType.Genders);
    }
}