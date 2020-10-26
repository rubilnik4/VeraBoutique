using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
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
        public async Task GetClothesShort_Ok()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            var clothesType = clothesTypeEntities.First().Name;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities,
                                                                                            clothesInformationEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = GetClothesTable(FirstClothesInformationOk(ClothesInformationEntitiesData.ClothesInformationEntities));
            var clothesShortEntityConverter = ClothesShortEntityConverter;

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    genderTable.Object, clothesTypeTable.Object,
                                                                    clothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    GetQueryableClothesShortService(QueryableToListOk).Object,
                                                                    GetQueryableInformationService(QueryableFirstOk).Object);

            var clothesResults = await clothesDatabaseService.GetClothesShorts(genderType, clothesType);
            var clothesShortDomains = clothesShortEntityConverter.FromEntities(clothesInformationEntities);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesShortDomains.Value));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetClothesShort_Exception()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            var clothesType = clothesTypeEntities.First().Name;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities,
                                                                                            clothesInformationEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = GetClothesTable(FirstClothesInformationOk(ClothesInformationEntitiesData.ClothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    genderTable.Object, clothesTypeTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                   GetQueryableClothesShortService(QueryableToListException).Object,
                                                                    GetQueryableInformationService(QueryableFirstOk).Object);

            var clothesResults = await clothesDatabaseService.GetClothesShorts(genderType, clothesType);

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
            var clothesTable = GetClothesTable(FirstClothesInformationOk(clothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    GetQueryableClothesShortService(QueryableToListOk).Object,
                                                                    GetQueryableInformationService(QueryableFirstOk).Object);

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
            var clothesTable = GetClothesTable(FirstClothesInformationNotFound());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    GetQueryableClothesShortService(QueryableToListOk).Object,
                                                                    GetQueryableInformationService(QueryableFirstOk).Object);

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
            var clothesTable = GetClothesTable(FirstClothesInformationException());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter,
                                                                    GetQueryableClothesShortService(QueryableToListOk).Object,
                                                                    GetQueryableInformationService(QueryableFirstException).Object);

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
        private static Mock<IClothesTable> GetClothesTable(Func<int, IQueryable<ClothesInformationEntity>> clothesInformationFunc) =>
            new Mock<IClothesTable>().        
            Void(mock => mock.Setup(clothesTable => clothesTable.Where(It.IsAny<int>())).
                              Returns(clothesInformationFunc));

        /// <summary>
        /// Таблица базы данных типа пола одежды
        /// </summary>
        private static Mock<IGenderTable> GetGenderTable(IEnumerable<GenderEntity> genderEntities) =>
            new Mock<IGenderTable>().
            Void(mock => mock.Setup(genderTable => genderTable.Where(It.IsAny<GenderType>())).
                              Returns((GenderType genderType) => genderEntities.
                                                                 Where(genderEntity => genderEntity.GenderType == genderType).
                                                                 AsQueryable()));

        /// <summary>
        /// Таблица базы данных типа пола одежды
        /// </summary>
        private static Mock<IClothesTypeTable> GetClothesTypeTable(IEnumerable<ClothesTypeEntity> clothesTypeEntities) =>
            new Mock<IClothesTypeTable>().
            Void(mock => mock.Setup(clothesTypeTable => clothesTypeTable.Where(It.IsAny<string>())).
                              Returns((string clothesType) => clothesTypeEntities.
                                                              Where(clothesTypeEntity => clothesTypeEntity.Name == clothesType).
                                                              AsQueryable()));

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
                                                  new GenderEntityConverter(), 
                                                  new ClothesTypeEntityConverter(new CategoryEntityConverter()), 
                                                  new ColorClothesEntityConverter(),
                                                  new SizeGroupEntityConverter(new SizeEntityConverter()));

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<int, ClothesShortEntity>> GetQueryableClothesShortService(Func<IEnumerable<ClothesShortEntity>, List<ClothesShortEntity>> toListFunc) =>
            new Mock<IQueryableService<int, ClothesShortEntity>>().
            Void(mock => mock.Setup(service => service.ToListAsync(It.IsAny<IEnumerable<ClothesShortEntity>>())).
                              ReturnsAsync(toListFunc));

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<int, ClothesInformationEntity>> GetQueryableInformationService(Func<IEnumerable<ClothesInformationEntity>, ClothesInformationEntity> firstFunc) =>
            new Mock<IQueryableService<int, ClothesInformationEntity>>().
            Void(mock => mock.Setup(service => service.FirstOrDefaultAsync(It.IsAny<IEnumerable<ClothesInformationEntity>>())).
                              ReturnsAsync(firstFunc));

        /// <summary>
        /// Функция преобразования в список для одежды
        /// </summary>
        private static Func<IEnumerable<ClothesShortEntity>, List<ClothesShortEntity>> QueryableToListOk =>
            clothesShortEntities => clothesShortEntities.ToList();

        /// <summary>
        /// Функция преобразования в список с ошибкой для одежды
        /// </summary>
        private static Func<IEnumerable<ClothesShortEntity>, List<ClothesShortEntity>> QueryableToListException =>
           _ => throw new Exception();

        /// <summary>
        /// Функция поиска для одежды
        /// </summary>
        private static Func<IEnumerable<ClothesInformationEntity>, ClothesInformationEntity> QueryableFirstOk =>
            clothesShortEntities => clothesShortEntities.First();

        /// <summary>
        /// Функция поиска с ошибкой для одежды
        /// </summary>
        private static Func<IEnumerable<ClothesInformationEntity>, ClothesInformationEntity> QueryableFirstException =>
           _ => throw new Exception();
    }
}