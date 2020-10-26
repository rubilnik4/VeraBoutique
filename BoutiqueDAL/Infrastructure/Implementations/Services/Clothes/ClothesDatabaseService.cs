using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис одежды в базе данных
    /// </summary>
    public class ClothesDatabaseService: DatabaseService<int, IClothesInformationDomain, IClothesInformationEntity, ClothesInformationEntity>,
                                         IClothesDatabaseService
    {
        public ClothesDatabaseService(IDatabase database, 
                                      IClothesTable clothesTable, IGenderTable genderTable, IClothesTypeTable clothesTypeTable,
                                      IClothesShortEntityConverter clothesShortEntityConverter,
                                      IClothesInformationEntityConverter clothesInformationEntityConverter,
                                      IQueryableService<int, ClothesShortEntity> queryableClothesShortService,
                                      IQueryableService<int, ClothesInformationEntity> queryableClothesInformationService)
          : base(database, clothesTable, clothesInformationEntityConverter)
        {
            _clothesTable = clothesTable;
            _genderTable = genderTable;
            _clothesTypeTable = clothesTypeTable;
            _clothesShortEntityConverter = clothesShortEntityConverter;
            _clothesInformationEntityConverter = clothesInformationEntityConverter;
            _queryableClothesShortService = queryableClothesShortService;
            _queryableClothesInformationService = queryableClothesInformationService;
        }

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private readonly IClothesTable _clothesTable;

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private readonly IGenderTable _genderTable;

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private readonly IClothesTypeTable _clothesTypeTable;

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private readonly IClothesShortEntityConverter _clothesShortEntityConverter;

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private readonly IClothesInformationEntityConverter _clothesInformationEntityConverter;

        /// <summary>
        /// Сервис обработки запросов базы данных одежды
        /// </summary>
        private readonly IQueryableService<int, ClothesShortEntity> _queryableClothesShortService;

        /// <summary>
        /// Сервис обработки запросов базы данных информации об одежде
        /// </summary>
        private readonly IQueryableService<int, ClothesInformationEntity> _queryableClothesInformationService;

        /// <summary>
        /// Получить одежду без изображений по типу полу и типу одежды
        /// </summary>
        public async Task<IResultCollection<IClothesShortDomain>> GetClothesShorts(GenderType genderType, string clothesType) =>
            await ResultCollectionTryAsync(() => GetByGenderAndClothesType(genderType, clothesType),
                                           DatabaseErrors.TableAccessError(nameof(_clothesTable))).
            ResultCollectionBindOkTaskAsync(clothesShortDomains => _clothesShortEntityConverter.FromEntities(clothesShortDomains));

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        public async Task<IResultValue<IClothesInformationDomain>> GetIncludesById(int id) =>
            await ResultValueBindTryAsync(() => GetClothesInformationIncludesById(id).
                                                ToResultValueNullCheckTaskAsync(DatabaseErrors.ValueNotFoundError(id.ToString(),
                                                                                                                  nameof(IClothesTable))),
                                          DatabaseErrors.TableAccessError(nameof(_clothesTable))).
            ResultValueBindOkTaskAsync(clothesInformationDomain => _clothesInformationEntityConverter.FromEntity(clothesInformationDomain));


        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        private async Task<IReadOnlyCollection<ClothesShortEntity>> GetByGenderAndClothesType(GenderType genderType, string clothesType) =>
            await GetClothesInformationByGender(genderType).
            Join(GetClothesInformationByClothesType(clothesType),
                 clothesInformationGender => clothesInformationGender.Id,
                 clothesInformationClothesType => clothesInformationClothesType.Id,
                 (clothesInformationGender, clothesInformationClothesType) => clothesInformationGender).            
            Select(clothesInformationEntity => new ClothesShortEntity(clothesInformationEntity.Id, clothesInformationEntity.Name,
                                                                      clothesInformationEntity.Price, clothesInformationEntity.Image)).
            AsNoTracking().
            Map(clothesShortQuery => _queryableClothesShortService.ToListAsync(clothesShortQuery));

        /// <summary>
        /// Получить список информации об одежде по типу пола
        /// </summary>
        private IQueryable<ClothesInformationEntity> GetClothesInformationByGender(GenderType genderType) =>
            _genderTable.
            Where(genderType).
            Include(genderEntity => genderEntity.ClothesInformationEntities).
            SelectMany(genderEntity => genderEntity.ClothesInformationEntities);

        /// <summary>
        /// Получить список информации об одежде по типу пола
        /// </summary>
        private IQueryable<ClothesInformationEntity> GetClothesInformationByClothesType(string clothesType) =>
            _clothesTypeTable.
            Where(clothesType).
            Include(clothesTypeEntity => clothesTypeEntity.ClothesInformationEntities).
            SelectMany(genderEntity => genderEntity.ClothesInformationEntities);

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        private async Task<ClothesInformationEntity?> GetClothesInformationIncludesById(int id) =>
            await _clothesTable.Where(id).
            Include(clothesInformationEntity => clothesInformationEntity.ClothesColorCompositeEntities).
            ThenInclude(clothesColorCompositeEntity => clothesColorCompositeEntity.ColorClothesEntity).
            Include(clothesInformationEntity => clothesInformationEntity.ClothesSizeGroupCompositeEntities).
            ThenInclude(clothesSizeGroupCompositeEntity => clothesSizeGroupCompositeEntity.SizeGroupEntity).
            ThenInclude(sizeGroupEntity => sizeGroupEntity!.SizeGroupCompositeEntities).
            ThenInclude(sizeGroupCompositeEntity => sizeGroupCompositeEntity.SizeEntity).
            AsNoTracking().
            Map(clothesInformationQuery => _queryableClothesInformationService.FirstOrDefaultAsync(clothesInformationQuery));
    }
}