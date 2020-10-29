using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис вида одежды в базе данных
    /// </summary>
    public class ClothesTypeDatabaseService : DatabaseService<string, IClothesTypeDomain, IClothesTypeEntity, ClothesTypeEntity>, 
                                              IClothesTypeDatabaseService
    {
        public ClothesTypeDatabaseService(IDatabase database,
                                          ICategoryDatabaseService categoryDatabaseService,
                                          IClothesTypeTable clothesTypeTable, IGenderTable genderTable, ICategoryTable categoryTable,
                                          IClothesTypeEntityConverter clothesTypeEntityConverter)
            : base(database, clothesTypeTable, clothesTypeEntityConverter)
        {
            _categoryDatabaseService = categoryDatabaseService;
            _genderTable = genderTable;
            _categoryTable = categoryTable;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly ICategoryDatabaseService _categoryDatabaseService;

        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        private readonly IGenderTable _genderTable;

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private readonly ICategoryTable _categoryTable;

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeEntityConverter _clothesTypeEntityConverter;

        public override async Task<IResultError> CheckEntities(IEnumerable<IClothesTypeDomain> clothesTypeDomains) =>
            await base.CheckEntities(clothesTypeDomains).
            ResultErrorBindOkBindAsync(() => _categoryDatabaseService.CheckEntities(clothesTypeDomains.Select(clothesType => clothesType.CategoryDomain)));

        /// <summary>
        /// Получить вид одежды по типу пола и категории
        /// </summary>
        public async Task<IResultCollection<IClothesTypeDomain>> GetByGenderCategory(GenderType genderType, string category) =>
            await ResultCollectionTryAsync(() => GetClothesTypes(genderType, category),
                                           DatabaseErrors.TableAccessError(nameof(_genderTable))).
            ResultCollectionBindOkTaskAsync(clothesTypes => _clothesTypeEntityConverter.FromEntities(clothesTypes));

        /// <summary>
        /// Получить вид одежды
        /// </summary>
        private async Task<IReadOnlyCollection<ClothesTypeEntity>> GetClothesTypes(GenderType genderType, string category) =>
            await GetClothesTypeByGender(genderType).
            Join(GetClothesTypeByCategory(category),
                 clothesTypeGender => clothesTypeGender.Name,
                 clothesTypeCategory => clothesTypeCategory.Name,
                 (clothesTypeGender, clothesTypeCategory) => clothesTypeGender).
            AsNoTracking().
            ToListAsync();

        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        private IQueryable<ClothesTypeEntity> GetClothesTypeByGender(GenderType genderType) =>
            _genderTable.Where(genderType).
            Include(genderEntity => genderEntity.ClothesTypeGenderEntities).
            SelectMany(genderEntity => genderEntity.ClothesTypeGenderEntities).
            Select(clothesTypeGenderEntity => clothesTypeGenderEntity.ClothesTypeEntity!);

        /// <summary>
        /// Получить вид одежды по категории
        /// </summary>
        private IQueryable<ClothesTypeEntity> GetClothesTypeByCategory(string category) =>
           _categoryTable.Where(category).
           Include(categoryEntity => categoryEntity.ClothesTypeEntities).
           SelectMany(categoryEntity => categoryEntity.ClothesTypeEntities);
    }
}