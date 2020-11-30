using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
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
    public class ClothesControllerTest
    {
        /// <summary>
        /// Получить одежду без картинок. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetClothes_Ok()
        {
            var clothesShortDomains = ClothesShortDomainsOk;
            var clothesDomain = ClothesDomainOk;
            var genderType = clothesDomain.Value.Gender.GenderType;
            var clothesType = clothesDomain.Value.ClothesTypeShort.Name;
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains, clothesDomain);
            var clothesShortTransferConverter = ClothesTransferConverterMock.ClothesShortTransferConverter;
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               clothesShortTransferConverter,
                                                               ClothesTransferConverterMock.ClothesTransferConverter);

            var clothesShortTransfers = await clothesShortController.GetClothesShorts(genderType, clothesType);
            var clothesShortAfter = clothesShortTransferConverter.FromTransfers(clothesShortTransfers.Value);

            Assert.True(clothesShortAfter.Value.SequenceEqual(clothesShortDomains.Value));
        }

        /// <summary>
        /// Получить одежду без картинок. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesShortDomains = new ResultCollection<IClothesShortDomain>(initialError);
            var clothesDomain = ClothesDomainOk;
            var genderType = clothesDomain.Value.Gender.GenderType;
            var clothesType = clothesDomain.Value.ClothesTypeShort.Name;
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains, clothesDomain);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesTransferConverterMock.ClothesShortTransferConverter,
                                                               ClothesTransferConverterMock.ClothesTransferConverter);

            var actionResult = await clothesShortController.GetClothesShorts(genderType, clothesType);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Ok()
        {
            var clothesDomain = ClothesDomainOk;
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesDomain);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesTransferConverterMock.ClothesShortTransferConverter,
                                                               clothesTransferConverter);

            var clothesTransfer = await clothesShortController.GetIncludesById(clothesDomain.Value.Id);
            var clothesAfter = clothesTransferConverter.FromTransfer(clothesTransfer.Value);

            Assert.True(clothesAfter.Value.Equals(clothesDomain.Value));
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetIncludesById_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesDomain = new ResultValue<IClothesDomain>(initialError);
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesDomain);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesTransferConverterMock.ClothesShortTransferConverter,
                                                               ClothesTransferConverterMock.ClothesTransferConverter);

            var actionResult = await clothesShortController.GetIncludesById(ClothesDomainOk.Value.Id);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить группу размеров одежды совместно с размерами. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetIncludesById_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var clothesDomain = new ResultValue<IClothesDomain>(initialError);
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesDomain);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesTransferConverterMock.ClothesShortTransferConverter,
                                                               ClothesTransferConverterMock.ClothesTransferConverter);

            var actionResult = await clothesShortController.GetIncludesById(ClothesDomainOk.Value.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesShortDomain> clothesShortDomains,
                                                                               IResultValue<IClothesDomain> clothesDomain) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetClothesShorts(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesShortDomains)).
            Void(mock => mock.Setup(service => service.GetIncludesById(It.IsAny<int>())).ReturnsAsync(clothesDomain));

        /// <summary>
        /// Данные одежды. Корректный вариант
        /// </summary>
        private static ResultCollection<IClothesShortDomain> ClothesShortDomainsOk =>
            new ResultCollection<IClothesShortDomain>(ClothesData.ClothesShortDomains);

        /// <summary>
        /// Данные информации об одежде. Корректный вариант
        /// </summary>
        private static ResultValue<IClothesDomain> ClothesDomainOk =>
            new ResultValue<IClothesDomain>(ClothesData.ClothesDomains.First());
    }
}