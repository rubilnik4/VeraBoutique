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
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных
    /// </summary>
    public class SizeGroupDatabaseService : DatabaseService<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>,
                                            ISizeGroupDatabaseService
    {
        public SizeGroupDatabaseService(IDatabase database, ISizeGroupTable sizeGroupTable,
                                        ISizeGroupEntityConverter sizeGroupEntityConverter)
            : base(database, sizeGroupTable, sizeGroupEntityConverter)
        {
            _sizeGroupTable = sizeGroupTable;
            _sizeGroupEntityConverter = sizeGroupEntityConverter;
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
        /// Получить группу размеров совместно со списком размеров
        /// </summary>
        public async Task<IResultCollection<ISizeGroupDomain>> GetSizeGroupsIncludeSize(ClothesSizeType clothesSizeType,
                                                                                        int sizeNormalize) =>
            await _sizeGroupTable.ToListAsync<(SizeType, string, ClothesSizeType, int), SizeGroupCompositeEntity>(
                sizeGroupEntity => sizeGroupEntity.SizeGroupCompositeEntities).
            ResultCollectionOkTaskAsync(sizeGroupEntities => _sizeGroupEntityConverter.FromEntities(sizeGroupEntities));
    }
}