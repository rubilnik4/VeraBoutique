using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных
    /// </summary>
    public class SizeGroupDatabaseService : DatabaseService<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>,
                                            ISizeGroupDatabaseService
    {
        public SizeGroupDatabaseService(IDatabase database, ISizeGroupTable sizeGroupTable,
                                        ISizeGroupEntityConverter sizeGroupEntityConverter,
                                        IQueryableService<(ClothesSizeType, int), SizeGroupEntity> queryableService)
            : base(database, sizeGroupTable, sizeGroupEntityConverter)
        {
            _sizeGroupTable = sizeGroupTable;
            _sizeGroupEntityConverter = sizeGroupEntityConverter;
            _queryableService = queryableService;
        }

        /// <summary>
        /// Таблица базы данных группы размеров
        /// </summary>
        private readonly ISizeGroupTable _sizeGroupTable;

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ISizeGroupEntityConverter _sizeGroupEntityConverter;

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private readonly IQueryableService<(ClothesSizeType, int), SizeGroupEntity> _queryableService;

        /// <summary>
        /// Получить группу размеров совместно со списком размеров
        /// </summary>
        public async Task<IResultValue<ISizeGroupDomain>> GetSizeGroupIncludeSize(ClothesSizeType clothesSizeType,
                                                                                  int sizeNormalize) =>
            await ResultValueBindTryAsync(() => GetSizeGroup(clothesSizeType, sizeNormalize).
                                            ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(clothesSizeType.ToString() + sizeNormalize, 
                                                                                                              nameof(ISizeGroupTable))),
                                           DatabaseErrors.TableAccessError(nameof(_sizeGroupTable))).
            ResultValueOkTaskAsync(sizeGroup => _sizeGroupEntityConverter.FromEntity(sizeGroup));

        /// <summary>
        /// Получить группу размеров совместно со списком размеров
        /// </summary>
        private async Task<SizeGroupEntity?> GetSizeGroup(ClothesSizeType clothesSizeType, int sizeNormalize) =>
            await _sizeGroupTable.Where((clothesSizeType, sizeNormalize)).
            Include(sizeGroupEntity => sizeGroupEntity.SizeGroupCompositeEntities).
            ThenInclude(sizeGroupComposite => sizeGroupComposite.SizeEntity).
            AsNoTracking().
            Map(sizeGroupQuery => _queryableService.FirstOrDefaultAsync(sizeGroupQuery));
    }
}