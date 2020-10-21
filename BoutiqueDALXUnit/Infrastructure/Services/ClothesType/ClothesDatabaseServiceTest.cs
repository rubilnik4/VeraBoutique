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
using Functional.FunctionalExtensions.Sync;
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
            var clothesShortEntities = EntityData.ClothesShortEntities;
          
            var clothesTable = GetClothesTable(clothesShortEntities);
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
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Ok()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesInformationEntities = EntityData.ClothesShortEntities;

            var clothesTable = GetClothesTable(clothesShortEntities);
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    QueryableClothesShortService.Object,
                                                                    QueryableInformationService.Object);

            var clothesResults = await clothesDatabaseService.GetIncludesById();

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesShortDomains));
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static Mock<IClothesTable> GetClothesTable(IEnumerable<ClothesShortEntity> clothesShortEntities) =>
            new Mock<IClothesTable>().
            Void(mock => mock.Setup(clothesTable => clothesTable.Select(clothesInformation => 
                                                        new ClothesShortEntity(clothesInformation.Id, clothesInformation.Name,
                                                                               clothesInformation.Price, clothesInformation.Image))).
                              Returns(clothesShortEntities.AsQueryable));

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesShortEntityConverter ClothesShortEntityConverter =>
            new ClothesShortEntityConverter();

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesInformationEntityConverter ClothesInformationEntityConverter =>
            new ClothesInformationEntityConverter(new ColorClothesEntityConverter(),
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