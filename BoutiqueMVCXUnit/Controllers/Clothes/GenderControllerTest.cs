using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueMVC.Controllers.Implementations.Clothes;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола. Тесты
    /// </summary>
    public class GenderControllerTest
    {
        /// <summary>
        /// Получить тип пола одежды с категорией. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_Ok()
        {
            var genderDomains = GenderData.GenderCategoryDomains;
            var gendersResult = new ResultCollection<IGenderCategoryDomain>(genderDomains);
            var clothesDatabaseService = GetGenderDatabaseService(gendersResult);
            var genderTransferConverter = GenderTransferConverterMock.GenderTransferConverter;
            var genderCategoryTransferConverter = GenderTransferConverterMock.GenderCategoryTransferConverter;
            var genderController = new GenderController(clothesDatabaseService.Object, genderTransferConverter,
                                                         genderCategoryTransferConverter);

            var actionResult = await genderController.GetGenderCategories();
            var gendersAfter = genderCategoryTransferConverter.FromTransfers(actionResult.Value);

            Assert.True(gendersAfter.Value.SequenceEqual(genderDomains));
        }

        /// <summary>
        /// Получить тип пола одежды с категорией. Ошибка
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_ErrorDatabase()
        {
            var initialError = ErrorData.ErrorTest;
            var gendersResult = new ResultCollection<IGenderCategoryDomain>(initialError);
            var clothesDatabaseService = GetGenderDatabaseService(gendersResult);
            var genderTransferConverter = GenderTransferConverterMock.GenderTransferConverter;
            var genderCategoryTransferConverter = GenderTransferConverterMock.GenderCategoryTransferConverter;
            var genderController = new GenderController(clothesDatabaseService.Object, genderTransferConverter,
                                                         genderCategoryTransferConverter);

            var actionResult = await genderController.GetGenderCategories();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }

        /// <summary>
        /// Сервис типа пола в базе данных
        /// </summary>
        private static Mock<IGenderDatabaseService> GetGenderDatabaseService(IResultCollection<IGenderCategoryDomain> genderDomains) =>
            new Mock<IGenderDatabaseService>().
            Void(mock => mock.Setup(service => service.GetGenderCategories()).
                              ReturnsAsync(genderDomains));
    }
}