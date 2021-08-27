using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы цвета одежды
    /// </summary>
    public class ColorClothesDatabaseValidateService : DatabaseValidateService<string, IColorDomain, ColorEntity>,
                                                      IColorClothesDatabaseValidateService
    {
        public ColorClothesDatabaseValidateService(IColorTable colorTable)
            : base(colorTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(IColorDomain color) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateSizeName(color));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateSizeName(IColorDomain color) =>
            color.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => DatabaseFieldErrors.FieldNotValid<string>(color.Name, nameof(IColorTable)));
    }
}