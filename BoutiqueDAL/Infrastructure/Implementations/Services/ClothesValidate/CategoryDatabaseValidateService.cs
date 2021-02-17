using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
    public class CategoryDatabaseValidateService : DatabaseValidateService<string, ICategoryDomain, CategoryEntity>,
                                                   ICategoryDatabaseValidateService
    {
        public CategoryDatabaseValidateService(ICategoryTable categoryTable)
            : base(categoryTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        protected override IResultError ValidateModel(ICategoryDomain category) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(category));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(ICategoryDomain category) =>
            category.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<string, ICategoryDomain>(nameof(category.Name), category));
    }
}