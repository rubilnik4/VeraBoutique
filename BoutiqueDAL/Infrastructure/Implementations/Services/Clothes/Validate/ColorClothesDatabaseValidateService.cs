using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate
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
                _ => DatabaseFieldErrors.FieldNotValid(color.Name, nameof(IColorTable)));
    }
}