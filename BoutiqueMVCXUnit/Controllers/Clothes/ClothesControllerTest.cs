using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueMVC.Controllers.Implementations.Clothes;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
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
    public class ClothesControllerTest
    {
        /// <summary>
        /// Получить одежду без картинок. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetClothes_Ok()
        {
            var clothesDomains = ClothesData.ClothesMainDomains;
            var clothesDomain = clothesDomains.First();
            var genderType = clothesDomain.Gender.GenderType;
            var clothesType = clothesDomain.ClothesType.Name;
            var clothesResult = new ResultCollection<IClothesMainDomain>(clothesDomains);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesController = new ClothesController(clothesDatabaseService.Object, 
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter,
                                                          ClothesTransferConverterMock.ClothesTransferConverter);

            var clothesTransfers = await clothesController.GetClothes(genderType, clothesType);
            var actionResult = clothesTransferConverter.FromTransfers(clothesTransfers.Value);

            Assert.True(actionResult.Value.SequenceEqual(clothesDomains));
        }

        /// <summary>
        /// Получить одежду без картинок. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesDomains = new ResultCollection<IClothesDomain>(initialError);
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var genderType = clothesDomain.Gender.GenderType;
            var clothesType = clothesDomain.ClothesType.Name;
            var clothesDatabaseService = GetClothesDatabaseService(clothesDomains);
            var clothesController = new ClothesController(clothesDatabaseService.Object,
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter,
                                                          ClothesTransferConverterMock.ClothesTransferConverter);

            var actionResult = await clothesController.GetClothes(genderType, clothesType);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить изображение. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetImage_Ok()
        {
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var clotheId = clothesDomain.Id;
            var clothesResult = new ResultValue<byte[]>(clothesDomain.Image);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesController = new ClothesController(clothesDatabaseService.Object,
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter,
                                                          ClothesTransferConverterMock.ClothesTransferConverter);

            var imageResult = await clothesController.GetImage(clotheId);

            Assert.IsType<FileContentResult>(imageResult.Result);
            var fileContentResult = (FileContentResult)imageResult.Result;
            Assert.True(fileContentResult.FileContents.SequenceEqual(clothesDomain.Image));
        }

        /// <summary>
        /// Получить изображение. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetImage_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var clotheId = clothesDomain.Id;
            var clothesResult = new ResultValue<byte[]>(initialError);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesController = new ClothesController(clothesDatabaseService.Object,
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter,
                                                          ClothesTransferConverterMock.ClothesTransferConverter);

            var imageResult = await clothesController.GetImage(clotheId);

            Assert.IsType<BadRequestObjectResult>(imageResult.Result);
            var badRequest = (BadRequestObjectResult)imageResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить изображение. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetImage_NotFound()
        {
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var initialError = ErrorData.NotFoundError;
            var clothesResult = new ResultValue<byte[]>(initialError);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesController = new ClothesController(clothesDatabaseService.Object,
                                                       ClothesTransferConverterMock.ClothesMainTransferConverter,
                                                       ClothesTransferConverterMock.ClothesTransferConverter);

            var actionResult = await clothesController.GetImage(clothesDomain.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesDomain> clothesDomains) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetClothes(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesDomains));

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultValue<byte[]> image) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetImage(It.IsAny<int>())).
                              ReturnsAsync(image));
    }
}