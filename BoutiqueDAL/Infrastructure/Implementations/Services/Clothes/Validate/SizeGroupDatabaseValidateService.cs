using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate
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
                sizeNormalized => sizeNormalized is >= SizeGroupBase.SIZE_NORMALIZE_MIN and <= SizeGroupBase.SIZE_NORMALIZE_MAX,
                _ => DatabaseFieldErrors.FieldRangeNotValid(SizeGroupBase.SIZE_NORMALIZE_MIN, SizeGroupBase.SIZE_NORMALIZE_MAX,
                                                       sizeGroupMain.SizeNormalize, nameof(ISizeGroupTable)));

        /// <summary>
        /// Проверка размеров
        /// </summary>
        private IResultError ValidateSizes(ISizeGroupMainDomain sizeGroupMain) =>
            _sizeDatabaseValidateService.ValidateQuantity(sizeGroupMain.Sizes);
    }
}