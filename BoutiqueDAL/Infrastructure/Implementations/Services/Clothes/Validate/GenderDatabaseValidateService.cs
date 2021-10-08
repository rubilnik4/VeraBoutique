using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы пола одежды
    /// </summary>
    public class GenderDatabaseValidateService : DatabaseValidateService<GenderType, IGenderDomain, GenderEntity>,
                                                 IGenderDatabaseValidateService
    {
        public GenderDatabaseValidateService(IGenderTable genderTable)
            : base(genderTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(IGenderDomain gender) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateSizeName(gender));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateSizeName(IGenderDomain gender) =>
            gender.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => DatabaseFieldErrors.FieldNotValid(gender.Name, nameof(IGenderTable)));
    }
}