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
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueBindTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис одежды в базе данных
    /// </summary>
    public class ClothesDatabaseService : DatabaseService<int, IClothesInformationDomain, IClothesInformationEntity, ClothesInformationEntity>,
                                         IClothesDatabaseService
    {
        public ClothesDatabaseService(IDatabase database,
                                      IGenderDatabaseService genderDatabaseService, IClothesTypeDatabaseService clothesTypeDatabaseService,
                                      IClothesTable clothesTable, IGenderTable genderTable, IClothesTypeTable clothesTypeTable,
                                      IClothesShortEntityConverter clothesShortEntityConverter,
                                      IClothesInformationEntityConverter clothesInformationEntityConverter)
          : base(database, clothesTable, clothesInformationEntityConverter)
        {
            _genderDatabaseService = genderDatabaseService;
            _clothesTypeDatabaseService = clothesTypeDatabaseService;
            _clothesTable = clothesTable;
            _genderTable = genderTable;
            _clothesTypeTable = clothesTypeTable;
            _clothesShortEntityConverter = clothesShortEntityConverter;
            _clothesInformationEntityConverter = clothesInformationEntityConverter;
        }

        /// <summary>
        /// Сервис типа пола одежды в базе данных
        /// </summary>
        private readonly IGenderDatabaseService _genderDatabaseService;

        /// <summary>
        /// Сервис типа пола одежды в базе данных
        /// </summary>
        private readonly IClothesTypeDatabaseService _clothesTypeDatabaseService;

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

        public override async Task<IResultCollection<int>> Post(IReadOnlyCollection<IClothesInformationDomain> clothesInformationDomains) =>
            _genderDatabaseService.CheckEntities(clothesInformationDomains.Select(clothes => clothes.Gender)).
            ResultErrorBindOkBindAsync(() => _clothesTypeDatabaseService.CheckEntities(clothesInformationDomains.Select(clothes => clothes.ClothesType)))
            
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
            ToListAsync();

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
            FirstOrDefaultAsync();
    }
}