using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.EntityFrameworkCore;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис одежды в базе данных
    /// </summary>
    public class ClothesDatabaseService : DatabaseService<int, IClothesMainDomain, ClothesEntity>, IClothesDatabaseService
    {
        public ClothesDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                      IClothesDatabaseValidateService clothesDatabaseValidateService,
                                      IClothesEntityConverter clothesEntityConverter,
                                      IClothesDetailEntityConverter clothesDetailEntityConverter,
                                      IClothesMainEntityConverter clothesMainEntityConverter,
                                      IClothesImageEntityConverter clothesImageEntityConverter)
          : base(boutiqueDatabase, boutiqueDatabase.ClothesTable, clothesDatabaseValidateService, clothesMainEntityConverter)
        {
            _clothesTable = boutiqueDatabase.ClothesTable;
            _clothesEntityConverter = clothesEntityConverter;
            _clothesDetailEntityConverter = clothesDetailEntityConverter;
            _clothesImageEntityConverter = clothesImageEntityConverter;
        }

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private readonly IClothesTable _clothesTable;

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private readonly IClothesEntityConverter _clothesEntityConverter;

        /// <summary>
        /// Преобразования модели уточненной информации об одежде в модель базы данных
        /// </summary>
        private readonly IClothesDetailEntityConverter _clothesDetailEntityConverter;

        /// <summary>
        /// Преобразования модели изображения в модель базы данных
        /// </summary>
        private readonly IClothesImageEntityConverter _clothesImageEntityConverter;

        /// <summary>
        /// Получить одежду без изображений по типу полу и типу одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDomain>> GetClothes(GenderType genderType, string clothesType) =>
            await _clothesTable.
            FindsExpressionAsync(clothes => clothes.Where(clothesEntity => clothesEntity.GenderType == genderType &&
                                                                           clothesEntity.ClothesTypeName == clothesType)).
            ResultCollectionBindOkTaskAsync(clothesEntities => _clothesEntityConverter.FromEntities(clothesEntities));

        /// <summary>
        /// Получить уточненную информацию об одежде по типу полу и типу одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDetailDomain>> GetClothesDetails(GenderType genderType, string clothesType) =>
            await _clothesTable.
            FindsExpressionAsync(clothes => clothes.Where(clothesEntity => clothesEntity.GenderType == genderType &&
                                                                           clothesEntity.ClothesTypeName == clothesType).
                                                    Include(clothesEntity => clothesEntity.ClothesColorComposites).
                                                    ThenInclude(clothesColor => clothesColor.ColorClothes).
                                                    Include(clothesEntity => clothesEntity.ClothesSizeGroupComposites).
                                                    ThenInclude(clothesColor => clothesColor.SizeGroup).
                                                    ThenInclude(sizeGroups => sizeGroups!.SizeGroupComposites).
                                                    ThenInclude(sizeComposites => sizeComposites.Size)).
            ResultCollectionBindOkTaskAsync(clothesEntities => _clothesDetailEntityConverter.FromEntities(clothesEntities));

        /// <summary>
        /// Получить изображение одежды по идентификатору
        /// </summary>
        public async Task<IResultValue<byte[]>> GetImage(int id) =>
             await _clothesTable.
             FindExpressionAsync(clothes => clothes.Where(clothesEntity => clothesEntity.Id == id).
                                                    Include(clothesEntity => clothesEntity.ClothesImages).
                                                    SelectMany(imageEntity => imageEntity.ClothesImages!).
                                                    FirstOrDefaultAsync(imageEntity => imageEntity.IsMain).
                                                    MapTaskAsync(entity => (ClothesImageEntity?)entity),
                                 id).
             ResultValueOkTaskAsync(imageEntity => imageEntity.Image);

        /// <summary>
        /// Получить изображение одежды по идентификатору
        /// </summary>
        public async Task<IResultCollection<IClothesImageDomain>> GetImages(int id) =>
             await _clothesTable.
             FindsExpressionAsync(clothes => clothes.Where(clothesEntity => clothesEntity.Id == id).
                                                     Include(clothesEntity => clothesEntity.ClothesImages).
                                                     SelectMany(imageEntity => imageEntity.ClothesImages!)).
             ResultCollectionBindOkTaskAsync(imageEntities => _clothesImageEntityConverter.FromEntities(imageEntities));
    }
}