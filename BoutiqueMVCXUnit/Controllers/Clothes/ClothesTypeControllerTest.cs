using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueMVC.Controllers.Implementations.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи вида одежды. Тесты
    /// </summary>
    public class ClothesTypeControllerTest
    {
        /// <summary>
        /// Получить вид одежды по типу пола. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetByGender_Ok()
        {
            const GenderType genderType = GenderType.Male;
            string category = CategoryData.CategoryDomains.First().Name;
            var clothesTypeDomains = new ResultCollection<IClothesTypeShortDomain>(ClothesTypeData.ClothesTypeShortDomains);
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeShortTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeShortTransferConverter;
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object,
                                                                  clothesTypeTransferConverter,
                                                                  clothesTypeShortTransferConverter);

            var clothesTypeTransfers = await clothesTypeController.GetByGenderCategory(genderType, category);
            var clothesTypeAfter = clothesTypeShortTransferConverter.FromTransfers(clothesTypeTransfers.Value);

            Assert.True(clothesTypeAfter.Value.SequenceEqual(clothesTypeDomains.Value));
        }

        /// <summary>
        /// Получить вид одежды по типу пола. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            const GenderType genderType = GenderType.Male;
            string category = CategoryData.CategoryDomains.First().Name;
            var initialError = ErrorData.DatabaseError;
            var clothesTypeDomains = new ResultCollection<IClothesTypeShortDomain>(initialError);
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeShortTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeShortTransferConverter;
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object,
                                                                  clothesTypeTransferConverter, 
                                                                  clothesTypeShortTransferConverter);

            var actionResult = await clothesTypeController.GetByGenderCategory(genderType, category);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить вид одежды по типу пола. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_NotFound()
        {
            const GenderType genderType = GenderType.Male;
            string category = CategoryData.CategoryDomains.First().Name;
            var initialError = ErrorData.NotFoundError;
            var clothesTypeDomains = new ResultCollection<IClothesTypeShortDomain>(initialError);
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeShortTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeShortTransferConverter;
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object, 
                                                                  clothesTypeTransferConverter,
                                                                  clothesTypeShortTransferConverter);

            var actionResult = await clothesTypeController.GetByGenderCategory(genderType, category);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private static Mock<IClothesTypeDatabaseService> GetClothesTypeDatabaseService(IResultCollection<IClothesTypeShortDomain> clothesTypeDomains) =>
            new Mock<IClothesTypeDatabaseService>().
            Void(mock => mock.Setup(service => service.GetByGenderCategory(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesTypeDomains));
    }
}