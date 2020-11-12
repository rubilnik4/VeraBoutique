using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
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
        public ClothesTypeDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                          IClothesTypeEntityConverter clothesTypeEntityConverter,
                                          IClothesTypeShortEntityConverter clothesTypeShortEntityConverter)
            : base(boutiqueDatabase, boutiqueDatabase.ClotheTypeTable, clothesTypeEntityConverter)
        {
            _genderTable = boutiqueDatabase.GendersTable;
            _categoryTable = boutiqueDatabase.CategoryTable;
            _clothesTypeShortEntityConverter = clothesTypeShortEntityConverter;
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
        private readonly IClothesTypeShortEntityConverter _clothesTypeShortEntityConverter;

        /// <summary>
        /// Получить вид одежды по типу пола и категории
        /// </summary>
        public async Task<IResultCollection<IClothesTypeShortDomain>> GetByGenderCategory(GenderType genderType, string category) =>
            await ResultCollectionTryAsync(() => GetClothesTypes(genderType, category),
                                           DatabaseErrors.TableAccessError(nameof(_genderTable))).
            ResultCollectionBindOkTaskAsync(clothesTypeShorts => _clothesTypeShortEntityConverter.FromEntities(clothesTypeShorts));

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        protected override IQueryable<ClothesTypeEntity> ValidateFilter(IQueryable<ClothesTypeEntity> entities,
                                                                     IReadOnlyCollection<IClothesTypeDomain> domains) =>
            base.ValidateFilter(entities, domains).
            Include(clothesType => clothesType.ClothesTypeGenderComposites).
            Where(entity => entity.ClothesTypeGenderComposites.
                            Select(clothesTypeGender => clothesTypeGender.GenderType).
                            SequenceEqual(domains.First(domain => domain.Id == entity.Id).Genders.
                                                  Select(gender => gender.GenderType)));

        /// <summary>
        /// Получить вид одежды
        /// </summary>
        private async Task<IReadOnlyCollection<ClothesTypeShortEntity>> GetClothesTypes(GenderType genderType, string category) =>
            await GetClothesTypeByGender(genderType).
            Join(GetClothesTypeByCategory(category),
                 clothesTypeGender => clothesTypeGender.Name,
                 clothesTypeCategory => clothesTypeCategory.Name,
                 (clothesTypeGender, clothesTypeCategory) => clothesTypeGender).
            Select(clothesType => new ClothesTypeShortEntity(clothesType.Name, clothesType.CategoryName, clothesType.Category,
                                                             clothesType.Clothes)).
            AsNoTracking().
            ToListAsync();

        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        private IQueryable<ClothesTypeEntity> GetClothesTypeByGender(GenderType genderType) =>
            _genderTable.Where(genderType).
            Include(genderEntity => genderEntity.ClothesTypeGenderComposites).
            SelectMany(genderEntity => genderEntity.ClothesTypeGenderComposites).
            Select(clothesTypeGenderEntity => clothesTypeGenderEntity.ClothesType!);

        /// <summary>
        /// Получить вид одежды по категории
        /// </summary>
        private IQueryable<ClothesTypeEntity> GetClothesTypeByCategory(string category) =>
           _categoryTable.Where(category).
           Include(categoryEntity => categoryEntity.ClothesTypes).
           SelectMany(categoryEntity => categoryEntity.ClothesTypes);
    }
}