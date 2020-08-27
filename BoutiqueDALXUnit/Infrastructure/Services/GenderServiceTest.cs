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
using Functional.Models.Implementations.Result;
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
            var genderEntities = EntityData.GetGenders();
            var gendersTableMock = new Mock<IDatabaseTable<GenderEntity>>();
            gendersTableMock.Setup(gendersTable => gendersTable.ToListAsync()).ReturnsAsync(genderEntities);

            var boutiqueDatabaseMock = new Mock<IBoutiqueDatabase>();
            boutiqueDatabaseMock.Setup(boutiqueDatabase => boutiqueDatabase.GendersTable).Returns(gendersTableMock.Object);

            var boutiqueDatabaseResult = new ResultValue<IBoutiqueDatabase>(boutiqueDatabaseMock.Object);
            var genderService = new GenderService(boutiqueDatabaseResult);

            var gendersResult = await genderService.GetGenders();
            var gendersOriginal = genderEntities.Select(GenderEntityConverter.FromEntity).ToList();
            
            Assert.True(gendersResult.OkStatus);
            Assert.True(gendersResult.Value.CompareByFunc(gendersOriginal, (genderFromDb, gender) => genderFromDb.Equals(gender)));
        }
    }
}