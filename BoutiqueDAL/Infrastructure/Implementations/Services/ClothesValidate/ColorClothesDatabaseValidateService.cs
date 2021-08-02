using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы цвета одежды
    /// </summary>
    public class ColorClothesDatabaseValidateService : DatabaseValidateService<string, IColorDomain, ColorEntity>,
                                                      IColorClothesDatabaseValidateService
    {
        public ColorClothesDatabaseValidateService(IColorClothesTable colorClothesTable)
            : base(colorClothesTable)
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
                _ => ModelsErrors.FieldNotValid<string, IColorDomain>(nameof(color.Name), color));
    }
}