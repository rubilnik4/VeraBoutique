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
    /// Сервис проверки данных из базы размера одежды
    /// </summary>
    public class SizeDatabaseValidateService : DatabaseValidateService<int, ISizeDomain, SizeEntity>,
                                               ISizeDatabaseValidateService
    {
        public SizeDatabaseValidateService(ISizeTable sizeTable)
            : base(sizeTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(ISizeDomain size) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(size));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(ISizeDomain size) =>
            size.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => DatabaseFieldErrors.FieldNotValid(size.Name, nameof(ISizeTable)));
    }
}