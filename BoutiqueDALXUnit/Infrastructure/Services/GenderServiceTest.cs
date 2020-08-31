using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDALXUnit.Data;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для типа пола одежды. Тесты
    /// </summary>
    public class GenderServiceTest
    {
        /// <summary>
        /// Проверить получение типа пола
        /// </summary>
        [Fact]
        public async Task GetGenders_OK()
        {
            var genderEntities = new ResultCollection<GenderEntity>(EntityData.GetGenderEntities());
            var gendersTableMock = new Mock<IDatabaseTable<GenderEntity>>();
            var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
            gendersTableMock.Setup(gendersTable => gendersTable.ToListAsync()).ReturnsAsync(genderEntities);
            boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var gendersResult = await genderService.GetGenders();
            var gendersOriginal = genderEntities.Value.Select(GenderEntityConverter.FromEntity).ToList();

            Assert.True(gendersResult.OkStatus);
            Assert.True(gendersResult.Value.CompareByFunc(gendersOriginal, (genderFromDb, gender) => genderFromDb.Equals(gender)));
        }

        /// <summary>
        /// Проверить получение типа пола. Возврат с ошибкой базы данных
        /// </summary>
        [Fact]
        public async Task GetGenders_ErrorDatabase()
        {
            var errorInitial = ErrorDatabase;
            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(errorInitial);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var gendersResult = await genderService.GetGenders();

            Assert.True(gendersResult.HasErrors);
            Assert.True(gendersResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить получение типа пола
        /// </summary>
        [Fact]
        public async Task GetGenders_ErrorDataTable()
        {
            var errorInitial = ErrorDatabaseTable;
            var genderEntities = new ResultCollection<GenderEntity>(errorInitial);
            var gendersTableMock = new Mock<IDatabaseTable<GenderEntity>>();
            var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
            gendersTableMock.Setup(gendersTable => gendersTable.ToListAsync()).ReturnsAsync(genderEntities);
            boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var gendersResult = await genderService.GetGenders();

            Assert.True(gendersResult.HasErrors);
            Assert.True(gendersResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить запись типа пола
        /// </summary>
        [Fact]
        public async Task UploadGenders_OK()
        {
            var gendersTableMock = new Mock<IDatabaseTable<GenderEntity>>();
            IEnumerable<GenderEntity> gendersEntitiesSaved = null;
            gendersTableMock.Setup(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>())).
                             Callback<IEnumerable<GenderEntity>>(gendersEntities => gendersEntitiesSaved = gendersEntities).
                             ReturnsAsync(new ResultError());

            var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
            boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var uploadGenders = EntityData.GetGenders();
            var uploadGenderEntities = uploadGenders.Select(GenderEntityConverter.ToEntity).ToList();
            var result = await genderService.UploadGenders(uploadGenders);

            Assert.True(result.OkStatus);
            gendersTableMock.Verify(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>()), Times.Once);
            Assert.True(uploadGenderEntities.CompareByFunc(gendersEntitiesSaved.ToList(), (first, second) => first.EqualEntity(second)));
        }

        /// <summary>
        /// Проверить запись типа пола. Возврат с ошибкой
        /// </summary>
        [Fact]
        public async Task UploadGenders_ErrorDatabase()
        {
            var errorInitial = ErrorDatabase;
            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(errorInitial);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var uploadGenders = EntityData.GetGenders();
            var gendersResult = await genderService.UploadGenders(uploadGenders);

            Assert.True(gendersResult.HasErrors);
            Assert.True(gendersResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить запись типа пола
        /// </summary>
        [Fact]
        public async Task UploadGenders_ErrorDatabaseTable()
        {
            var errorInitial = ErrorDatabaseTable;
            var gendersTableMock = new Mock<IDatabaseTable<GenderEntity>>();
            gendersTableMock.Setup(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>())).
                             ReturnsAsync(new ResultError(errorInitial));

            var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
            boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var uploadGenders = EntityData.GetGenders();
            var result = await genderService.UploadGenders(uploadGenders);

            Assert.True(result.HasErrors);
            gendersTableMock.Verify(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>()), Times.Once);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Тестовая ошибка подключения к базе данных
        /// </summary>
        private static IErrorResult ErrorDatabase =>
            new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Тестовая ошибка базы");

        /// <summary>
        /// Тестовая ошибка подключения к базе данных
        /// </summary>
        private static IErrorResult ErrorDatabaseTable =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, "Тестовая ошибка таблицы базы");
    }
}