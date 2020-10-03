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
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
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
    public class ClothesTypeDatabaseService : DatabaseService<string, IClothesTypeDomain, ClothesTypeEntity>, 
                                              IClothesTypeDatabaseService
    {
        public ClothesTypeDatabaseService(IDatabase database, IClothesTypeTable clothesTypeTable,
                                          IGenderTable genderTable, ICategoryTable categoryTable,
                                          IClothesTypeEntityConverter clothesTypeEntityConverter)
            : base(database, clothesTypeTable, clothesTypeEntityConverter)
        {
            _genderTable = genderTable;
            _categoryTable = categoryTable;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
        }

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

        /// <summary>
        /// Получить вид одежды по типу пола и категории
        /// </summary>
        public async Task<IResultCollection<IClothesTypeDomain>> GetByGenderCategory(GenderType genderType, string category) =>
            await ResultCollectionTryAsync(() => GetClothesTypes(genderType, category),
                                           DatabaseErrors.TableAccessError(nameof(_genderTable))).
            ResultCollectionOkTaskAsync(clothesTypes => _clothesTypeEntityConverter.FromEntities(clothesTypes));

        private async Task<IReadOnlyCollection<ClothesTypeEntity>> GetClothesTypes(GenderType genderType, string category) =>
            await GetClothesTypeByGender(genderType).
            Join(GetClothesTypeByCategory(category),
                 clothesTypeGender => clothesTypeGender.Name,
                 clothesTypeCategory => clothesTypeCategory.Name,
                 (clothesTypeGender, clothesTypeCategory) => clothesTypeGender).
            AsNoTracking().
            ToListAsync();

        private IQueryable<ClothesTypeEntity> GetClothesTypeByGender(GenderType genderType) =>
            _genderTable.Where<(string, GenderType), ClothesTypeGenderEntity>(genderType, genderEntity => genderEntity.ClothesTypeGenderEntities).
            SelectMany(genderEntity => genderEntity.ClothesTypeGenderEntities).
            Select(clothesTypeGenderEntity => clothesTypeGenderEntity.ClothesTypeEntity!);

        private IQueryable<ClothesTypeEntity> GetClothesTypeByCategory(string category) =>
           _categoryTable.Where<string, ClothesTypeEntity>(category, categoryEntity => categoryEntity.ClothesTypeEntities).
           SelectMany(categoryEntity => categoryEntity.ClothesTypeEntities);
    }
}