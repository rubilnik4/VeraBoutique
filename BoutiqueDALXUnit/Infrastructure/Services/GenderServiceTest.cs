using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
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
        ///// <summary>
        ///// Проверить запись типа пола
        ///// </summary>
        //[Fact]
        //public async Task UploadGenders_OK()
        //{
        //    var uploadGenders = EntityData.GetGendersDomain();
        //    var genderIds = EntityData.GetGendersIds();
        //    var gendersTableMock = new Mock<IDatabaseTable<GenderType, GenderEntity>>();
        //    gendersTableMock.Setup(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>())).
        //                     ReturnsAsync(new ResultCollection<GenderType>(genderIds));

        //    var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
        //    boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

        //    var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
        //    var genderConverter = (IGenderEntityConverter)new GenderEntityConverter();
        //    var genderService = new GenderService(boutiqueDatabaseResult,
        //                                          boutiqueDatabaseResult.ResultValueOk(database => database.GendersTable),
        //                                          genderConverter);

        //    var resultIds = await genderService.Post(uploadGenders);

        //    Assert.True(resultIds.OkStatus);
        //    Assert.True(resultIds.Value.SequenceEqual(genderIds));
        //}

        ///// <summary>
        ///// Проверить запись типа пола. Возврат с ошибкой
        ///// </summary>
        //[Fact]
        //public async Task UploadGenders_ErrorDatabase()
        //{
        //    var errorInitial = ErrorDatabase;
        //    var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(errorInitial);
        //    var genderConverter = (IGenderEntityConverter)new GenderEntityConverter();
        //    var genderService = new GenderService(boutiqueDatabaseResult,
        //                                          boutiqueDatabaseResult.ResultValueOk(database => database.GendersTable),
        //                                          genderConverter);

        //    var uploadGenders = EntityData.GetGendersDomain();
        //    var gendersResult = await genderService.Post(uploadGenders);

        //    Assert.True(gendersResult.HasErrors);
        //    Assert.True(gendersResult.Errors.First().Equals(errorInitial));
        //}

        ///// <summary>
        ///// Проверить запись типа пола. Ошибка добавления данных
        ///// </summary>
        //[Fact]
        //public async Task UploadGenders_ErrorDatabaseTable()
        //{
        //    var uploadGenders = EntityData.GetGendersDomain();
        //    var errorInitial = ErrorDatabaseTable;
        //    var gendersTableMock = new Mock<IDatabaseTable<GenderType, GenderEntity>>();
        //    gendersTableMock.Setup(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>())).
        //                     ReturnsAsync(new ResultCollection<GenderType>(errorInitial));

        //    var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
        //    boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

        //    var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
        //    var genderConverter = (IGenderEntityConverter)new GenderEntityConverter();
        //    var genderService = new GenderService(boutiqueDatabaseResult,
        //                                          boutiqueDatabaseResult.ResultValueOk(database => database.GendersTable),
        //                                          genderConverter);

        //    var result = await genderService.Post(uploadGenders);

        //    Assert.True(result.HasErrors);
        //    gendersTableMock.Verify(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>()), Times.Once);
        //    Assert.True(result.Errors.First().Equals(errorInitial));
        //}

        ///// <summary>
        ///// Проверить запись типа пола. Ошибка сохранения данных
        ///// </summary>
        //[Fact]
        //public async Task UploadGenders_ErrorDatabaseSaving()
        //{
        //    var uploadGenders = EntityData.GetGendersDomain();
        //    var genderIds = EntityData.GetGendersIds();
        //    var errorInitial = ErrorDatabase;
        //    var gendersTableMock = new Mock<IDatabaseTable<GenderType, GenderEntity>>();
        //    gendersTableMock.Setup(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>())).
        //                     ReturnsAsync(new ResultCollection<GenderType>(genderIds));

        //    var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
        //    boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);
        //    boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.SaveChangesAsync()).
        //                         ReturnsAsync(new ResultError(errorInitial));

        //    var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
        //    var genderConverter = (IGenderEntityConverter)new GenderEntityConverter();
        //    var genderService = new GenderService(boutiqueDatabaseResult,
        //                                          boutiqueDatabaseResult.ResultValueOk(database => database.GendersTable),
        //                                          genderConverter);

        //    var result = await genderService.Post(uploadGenders);

        //    Assert.True(result.HasErrors);
        //    gendersTableMock.Verify(gendersTable => gendersTable.AddRangeAsync(It.IsAny<IEnumerable<GenderEntity>>()), Times.Once);
        //    Assert.True(result.Errors.First().Equals(errorInitial));
        //}
    }
}