using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
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
    /// Сервис проверки данных из базы группы размера одежды
    /// </summary>
    public class SizeGroupDatabaseValidateService : DatabaseValidateService<int, ISizeGroupMainDomain, SizeGroupEntity>,
                                                    ISizeGroupDatabaseValidateService
    {
        public SizeGroupDatabaseValidateService(ISizeGroupTable sizeGroupTable, 
                                                ISizeDatabaseValidateService sizeDatabaseValidateService)
            : base(sizeGroupTable)
        {
            _sizeDatabaseValidateService = sizeDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы размера одежды
        /// </summary>
        private readonly ISizeDatabaseValidateService _sizeDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(ISizeGroupMainDomain sizeGroupMain) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateSizeNormalized(sizeGroupMain)).
            ResultErrorBindOk(() => ValidateSizes(sizeGroupMain));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(ISizeGroupMainDomain sizeGroupMain) =>
             await new ResultError().
            ResultErrorBindOkAsync(() => _sizeDatabaseValidateService.ValidateFinds(sizeGroupMain.Sizes.Select(size => size.Id)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<ISizeGroupMainDomain> sizeGroupMains) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => sizeGroupMains.SelectMany(sizeGroup => sizeGroup.Sizes.Select(size => size.Id)).
                                                        Distinct().
                                         Map(ids => _sizeDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateSizeNormalized(ISizeGroupMainDomain sizeGroupMain) =>
            sizeGroupMain.SizeNormalize.ToResultValueWhere(
                sizeNormalized => sizeNormalized >= SizeGroupBase.SIZE_NORMALIZE_MIN && sizeNormalized <= SizeGroupBase.SIZE_NORMALIZE_MAX,
                _ => ModelsErrors.FieldNotValid<int, ISizeGroupDomain>(SizeGroupBase.SIZE_NORMALIZE_MIN, 
                                                                                               SizeGroupBase.SIZE_NORMALIZE_MAX,
                                                                                               nameof(sizeGroupMain.SizeNormalize),
                                                                                               sizeGroupMain));

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateSizes(ISizeGroupMainDomain sizeGroupMain) =>
            _sizeDatabaseValidateService.ValidateQuantity(sizeGroupMain.Sizes);
    }
}