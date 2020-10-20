using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
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
    /// Контроллер для получения и записи одежды. Тесты
    /// </summary>
    public class ClothesShortControllerTest
    {
        /// <summary>
        /// Получить одежду без картинок. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetWithoutImages_Ok()
        {
            var clothesShortDomains = new ResultCollection<IClothesShortDomain>(ClothesData.GetClothesShortDomain());
            var clothesShortDatabaseService = GetClothesShortDatabaseService(clothesShortDomains);
            var clothesTypeTransferConverter = ClothesShortTransferConverter;
            var clothesShortController = new ClothesShortController(clothesShortDatabaseService.Object, ClothesShortTransferConverter);

            var clothesShortTransfers = await clothesShortController.GetWithoutImages();
            var clothesShortAfter = clothesTypeTransferConverter.FromTransfers(clothesShortTransfers.Value);

            Assert.True(clothesShortAfter.SequenceEqual(clothesShortDomains.Value));
        }

        /// <summary>
        /// Получить одежду без картинок. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesShortDomains = new ResultCollection<IClothesShortDomain>(initialError);
            var clothesShortDatabaseService = GetClothesShortDatabaseService(clothesShortDomains);
            var clothesTypeTransferConverter = ClothesShortTransferConverter;
            var clothesShortController = new ClothesShortController(clothesShortDatabaseService.Object, clothesTypeTransferConverter);

            var actionResult = await clothesShortController.GetWithoutImages();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesShortDatabaseService> GetClothesShortDatabaseService(IResultCollection<IClothesShortDomain> clothesShortDomains) =>
            new Mock<IClothesShortDatabaseService>().
            Void(mock => mock.Setup(service => service.GetWithoutImages()).
                              ReturnsAsync(clothesShortDomains));

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        private static IClothesShortTransferConverter ClothesShortTransferConverter =>
            new ClothesShortTransferConverter();
    }
}