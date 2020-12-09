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
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
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
    public class SizeGroupDatabaseValidateService : DatabaseValidateService<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>,
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
        protected override IResultError ValidateModel(ISizeGroupDomain sizeGroup) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateSizeNormalized(sizeGroup)).
            ResultErrorBindOk(() => ValidateSizes(sizeGroup));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(ISizeGroupDomain sizeGroup) =>
             await new ResultError().
            ResultErrorBindOkAsync(() => _sizeDatabaseValidateService.ValidateFinds(sizeGroup.Sizes.Select(size => size.Id)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<ISizeGroupDomain> sizeGroups) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => sizeGroups.SelectMany(sizeGroup => sizeGroup.Sizes.Select(size => size.Id)).
                                                    Distinct().
                                         Map(ids => _sizeDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateSizeNormalized(ISizeGroupDomain sizeGroup) =>
            sizeGroup.SizeNormalize.ToResultValueWhere(
                sizeNormalized => sizeNormalized >= SizeGroupShortBase.SIZE_NORMALIZE_MIN && sizeNormalized <= SizeGroupShortBase.SIZE_NORMALIZE_MAX,
                _ => ModelsErrors.FieldNotValid<(ClothesSizeType, int), ISizeGroupDomain>(SizeGroupShortBase.SIZE_NORMALIZE_MIN, SizeGroupShortBase.SIZE_NORMALIZE_MAX,
                                                                                          nameof(sizeGroup.SizeNormalize), sizeGroup));

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateSizes(ISizeGroupDomain sizeGroup) =>
            _sizeDatabaseValidateService.ValidateQuantity(sizeGroup.Sizes);
    }
}