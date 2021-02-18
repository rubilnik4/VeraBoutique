using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
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
    public class ClothesDatabaseService : DatabaseService<int, IClothesMainDomain, ClothesEntity>,
                                          IClothesDatabaseService
    {
        public ClothesDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                      IClothesDatabaseValidateService clothesDatabaseValidateService,
                                      IClothesShortEntityConverter clothesShortEntityConverter,
                                      IClothesEntityConverter clothesEntityConverter)
          : base(boutiqueDatabase, boutiqueDatabase.ClothesTable, clothesDatabaseValidateService, clothesEntityConverter)
        {
            _clothesTable = boutiqueDatabase.ClothesTable;
            _genderTable = boutiqueDatabase.GendersTable;
            _clothesTypeTable = boutiqueDatabase.ClotheTypeTable;
            _clothesShortEntityConverter = clothesShortEntityConverter;
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
        /// Получить одежду без изображений по типу полу и типу одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDomain>> GetClothes(GenderType genderType, string clothesType) =>
            await ResultCollectionTryAsync(() => GetByGenderAndClothesType(genderType, clothesType),
                                           DatabaseErrors.TableAccessError(nameof(_clothesTable))).
            ResultCollectionBindOkTaskAsync(clothesShortDomains => _clothesShortEntityConverter.FromEntities(clothesShortDomains));

        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        private async Task<IReadOnlyCollection<ClothesEntity>> GetByGenderAndClothesType(GenderType genderType, string clothesType) =>
            await GetClothesByGender(genderType).
            Join(GetClothesByClothesType(clothesType),
                 clothesGender => clothesGender.Id,
                 clothesClothesType => clothesClothesType.Id,
                 (clothesGender, clothesClothesType) => clothesGender).
            AsNoTracking().
            ToListAsync();

        /// <summary>
        /// Получить список информации об одежде по типу пола
        /// </summary>
        private IQueryable<ClothesEntity> GetClothesByGender(GenderType genderType) =>
            _genderTable.
            Where(genderType).
            Include(genderEntity => genderEntity.Clothes).
            SelectMany(genderEntity => genderEntity.Clothes!);

        /// <summary>
        /// Получить список информации об одежде по типу пола
        /// </summary>
        private IQueryable<ClothesEntity> GetClothesByClothesType(string clothesType) =>
            _clothesTypeTable.
            Where(clothesType).
            Include(clothesTypeEntity => clothesTypeEntity.Clothes).
            SelectMany(genderEntity => genderEntity.Clothes!);
    }
}