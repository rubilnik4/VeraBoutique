using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
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
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public class CategoryDatabaseValidateService : DatabaseValidateService<string, ICategoryMainDomain, CategoryEntity>,
                                                   ICategoryDatabaseValidateService
    {
        public CategoryDatabaseValidateService(ICategoryTable categoryTable,
                                               IGenderDatabaseValidateService genderDatabaseValidateService)
            : base(categoryTable)
        {
            _genderDatabaseValidateService = genderDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private readonly IGenderDatabaseValidateService _genderDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(ICategoryMainDomain categoryMain) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(categoryMain)).
            ResultErrorBindOk(() => ValidateGenders(categoryMain));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(ICategoryMainDomain categoryMain) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => categoryMain.Genders.Select(gender => gender.Id).
                                         Map(ids => _genderDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<ICategoryMainDomain> categoryMains) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => categoryMains.SelectMany(category => category.Genders.Select(gender => gender.Id)).
                                                       Distinct().
                                         Map(ids => _genderDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(ICategoryMainDomain categoryMain) =>
            categoryMain.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => DatabaseFieldErrors.FieldNotValid(categoryMain.Name, nameof(ICategoryTable)));

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateGenders(ICategoryMainDomain categoryMain) =>
            _genderDatabaseValidateService.ValidateQuantity(categoryMain.Genders);

    }
}