using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
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
        protected override IResultError ValidateModel(ICategoryMainDomain categoryMain) =>
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
                _ => ModelsErrors.FieldNotValid<string, ICategoryDomain>(nameof(categoryMain.Name), categoryMain));

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateGenders(ICategoryMainDomain categoryMain) =>
            _genderDatabaseValidateService.ValidateQuantity(categoryMain.Genders);

    }
}