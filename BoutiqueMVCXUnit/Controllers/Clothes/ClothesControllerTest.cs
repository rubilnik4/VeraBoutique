using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
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
            var clothesResult = new ResultCollection<IClothesDomain>(clothesDomains);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesController = GetClothesController(clothesDatabaseService.Object);

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
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var actionResult = await clothesController.GetClothes(genderType, clothesType);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить одежду без картинок. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetClothesDetails_Ok()
        {
            var clothesDomains = ClothesData.ClothesDetailDomains;
            var clothesDomain = clothesDomains.First();
            var genderType = clothesDomain.GenderType;
            var clothesType = clothesDomain.Name;
            var clothesResult = new ResultCollection<IClothesDetailDomain>(clothesDomains);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesDetailTransferConverter;
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var clothesTransfers = await clothesController.GetClothesDetails(genderType, clothesType);
            var actionResult = clothesTransferConverter.FromTransfers(clothesTransfers.Value);

            Assert.True(actionResult.Value.SequenceEqual(clothesDomains));
        }

        /// <summary>
        /// Получить одежду без картинок. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGenderDetails_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesDomains = new ResultCollection<IClothesDetailDomain>(initialError);
            var clothesDomain = ClothesData.ClothesDetailDomains.First();
            var genderType = clothesDomain.GenderType;
            var clothesType = clothesDomain.Name;
            var clothesDatabaseService = GetClothesDatabaseService(clothesDomains);
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var actionResult = await clothesController.GetClothesDetails(genderType, clothesType);

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
            var image = clothesDomain.Images.First().Image;
            var clothesResult = new ResultValue<byte[]>(image);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var imageResult = await clothesController.GetImage(clotheId);

            Assert.IsType<FileContentResult>(imageResult.Result);
            var fileContentResult = (FileContentResult)imageResult.Result;
            Assert.True(fileContentResult.FileContents.SequenceEqual(image));
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
            var clothesController = GetClothesController(clothesDatabaseService.Object);

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
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var actionResult = await clothesController.GetImage(clothesDomain.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Получить изображения. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetImages_Ok()
        {
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var clotheId = clothesDomain.Id;
            var images = clothesDomain.Images;
            var clothesResult = new ResultCollection<IClothesImageDomain>(images);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesImageTransferConverter = ClothesImageTransferConverterMock.ClothesImageTransferConverter;
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var imageTransfers = await clothesController.GetImages(clotheId);
            var actionResult = clothesImageTransferConverter.FromTransfers(imageTransfers.Value);

            Assert.True(images.SequenceEqual(actionResult.Value));
        }

        /// <summary>
        /// Получить изображения. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetImages_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var clotheId = clothesDomain.Id;
            var clothesResult = new ResultCollection<IClothesImageDomain>(initialError);
            var clothesDatabaseService = GetClothesDatabaseService(clothesResult);
            var clothesController = GetClothesController(clothesDatabaseService.Object);

            var imageResult = await clothesController.GetImages(clotheId);

            Assert.IsType<BadRequestObjectResult>(imageResult.Result);
            var badRequest = (BadRequestObjectResult)imageResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
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
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesDetailDomain> clothesDetailDomains) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetClothesDetails(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesDetailDomains));

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultValue<byte[]> image) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetImage(It.IsAny<int>())).
                              ReturnsAsync(image));

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesImageDomain> images) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetImages(It.IsAny<int>())).
                              ReturnsAsync(images));


        /// <summary>
        /// Получить контроллер одежды
        /// </summary>
        private static ClothesController GetClothesController(IClothesDatabaseService clothesDatabaseService) =>
            new(clothesDatabaseService,
                ClothesTransferConverterMock.ClothesMainTransferConverter,
                ClothesTransferConverterMock.ClothesDetailTransferConverter,
                ClothesTransferConverterMock.ClothesTransferConverter,
                ClothesImageTransferConverterMock.ClothesImageTransferConverter);
    }
}