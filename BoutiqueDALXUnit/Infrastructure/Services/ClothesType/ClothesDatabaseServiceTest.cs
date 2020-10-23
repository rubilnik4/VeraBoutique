using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    /// <summary>
    /// Сервис одежды в базе данных. Тесты
    /// </summary>
    public class ClothesDatabaseServiceTest
    {
        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [Fact]
        public async Task GetWithoutImages_Ok()
        {
            var clothesShortDomains = ClothesData.ClothesShortDomains;
            var clothesShortEntities = ClothesShortEntitiesData.ClothesShortEntities;

            var clothesTable = GetClothesTable(GetClothesShortOk(clothesShortEntities),
                                               FirstClothesInformationOk(ClothesInformationEntitiesData.ClothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetWithoutImages();

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesShortDomains));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetWithoutImages_Exception()
        {
            var clothesTable = GetClothesTable(GetClothesShortException(),
                                               FirstClothesInformationOk(ClothesInformationEntitiesData.ClothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetWithoutImages();

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Ok()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var clothesTable = GetClothesTable(GetClothesShortOk(ClothesShortEntitiesData.ClothesShortEntities),
                                               FirstClothesInformationOk(clothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.Equals(clothesInformationDomains.Last()));
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_NotFound()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesTable = GetClothesTable(GetClothesShortOk(ClothesShortEntitiesData.ClothesShortEntities),
                                               FirstClothesInformationNotFound());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Exception()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesTable = GetClothesTable(GetClothesShortOk(ClothesShortEntitiesData.ClothesShortEntities),
                                               FirstClothesInformationException());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static Mock<IClothesTable> GetClothesTable(Func<IQueryable<ClothesShortEntity>> clothesShortFunc,
                                                           Func<int, IQueryable<ClothesInformationEntity>> clothesInformationFunc) =>
            new Mock<IClothesTable>().
            Void(mock => mock.Setup(clothesTable => clothesTable.Select(clothesInformation =>
                                                        new ClothesShortEntity(clothesInformation.Id, clothesInformation.Name,
                                                                               clothesInformation.Price, clothesInformation.Image))).
                              Returns(clothesShortFunc)).
            Void(mock => mock.Setup(clothesTable => clothesTable.Where(It.IsAny<int>())).
                              Returns(clothesInformationFunc));

        /// <summary>
        /// Функция выбора одежды
        /// </summary>
        private static Func<IQueryable<ClothesShortEntity>> GetClothesShortOk(IEnumerable<ClothesShortEntity> clothesShortEntities) =>
            clothesShortEntities.AsQueryable;

        /// <summary>
        /// Функция выбора одежды с ошибкой
        /// </summary>
        private static Func<IQueryable<ClothesShortEntity>> GetClothesShortException() =>
            () => throw new Exception();

        /// <summary>
        /// Функция поиска информации об одежде
        /// </summary>
        private static Func<int, IQueryable<ClothesInformationEntity>> FirstClothesInformationOk(IEnumerable<ClothesInformationEntity> clothesInformationEntities) =>
            id => clothesInformationEntities.Where(clothesInformation => clothesInformation.Id == id).AsQueryable();

        /// <summary>
        /// Функция поиска информации об одежде. Элемент не найден
        /// </summary>
        private static Func<int, IQueryable<ClothesInformationEntity>> FirstClothesInformationNotFound() =>
            _ => Enumerable.Empty<ClothesInformationEntity>().AsQueryable();

        /// <summary>
        /// Функция поиска информации об одежде. Ошибка
        /// </summary>
        private static Func<int, IQueryable<ClothesInformationEntity>> FirstClothesInformationException() =>
            _ => throw new Exception();

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesShortEntityConverter ClothesShortEntityConverter =>
            new ClothesShortEntityConverter();

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesInformationEntityConverter ClothesInformationEntityConverter =>
            new ClothesInformationEntityConverter(new ClothesShortEntityConverter(), 
                                                  new ClothesTypeEntityConverter(), 
                                                  new ColorClothesEntityConverter(),
                                                  new SizeGroupEntityConverter(new SizeEntityConverter()));

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<int, ClothesShortEntity>> QueryableClothesShortService =>
            new Mock<IQueryableService<int, ClothesShortEntity>>().
            Void(mock => mock.Setup(service => service.ToListAsync(It.IsAny<IEnumerable<ClothesShortEntity>>())).
                              ReturnsAsync((IEnumerable<ClothesShortEntity> clothesShortEntities) => clothesShortEntities.ToList()));

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<int, ClothesInformationEntity>> QueryableInformationService =>
            new Mock<IQueryableService<int, ClothesInformationEntity>>().
            Void(mock => mock.Setup(service => service.FirstOrDefaultAsync(It.IsAny<IEnumerable<ClothesInformationEntity>>())).
                              ReturnsAsync((IEnumerable<ClothesInformationEntity> clothesInformationEntities) => clothesInformationEntities.FirstOrDefault()));
    }
}