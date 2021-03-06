﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
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
        protected override IResultError ValidateModel(IGenderDomain gender) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateSizeName(gender));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateSizeName(IGenderDomain gender) =>
            gender.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<GenderType, IGenderDomain>(nameof(gender.Name), gender));
    }
}