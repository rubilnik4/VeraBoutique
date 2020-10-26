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

namespace MVCXUnit.Controllers.Clothes
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
        public async Task GetWithoutImages_Ok()
        {
            var clothesShortDomains = ClothesShortDomainsOk;
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains, ClothesInformationDomainOk);
            var clothesShortTransferConverter = ClothesShortTransferConverter;
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesShortTransferConverter, 
                                                               ClothesInformationTransferConverter);

            var clothesShortTransfers = await clothesShortController.GetClothesShorts();
            var clothesShortAfter = clothesShortTransferConverter.FromTransfers(clothesShortTransfers.Value);

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
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains, ClothesInformationDomainOk);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesShortTransferConverter,
                                                               ClothesInformationTransferConverter);

            var actionResult = await clothesShortController.GetClothesShorts();

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
            var clothesInformationDomain = ClothesInformationDomainOk;
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesInformationDomain);
            var clothesInformationTransferConverter = ClothesInformationTransferConverter;
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesShortTransferConverter,
                                                               ClothesInformationTransferConverter);

            var clothesInformationTransfer = await clothesShortController.GetIncludesById(clothesInformationDomain.Value.Id);
            var clothesInformationAfter = clothesInformationTransferConverter.FromTransfer(clothesInformationTransfer.Value);

            Assert.True(clothesInformationAfter.Equals(clothesInformationDomain.Value));
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetIncludesById_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var clothesInformationDomain = new ResultValue<IClothesInformationDomain>(initialError);
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesInformationDomain);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesShortTransferConverter,
                                                               ClothesInformationTransferConverter);

            var actionResult = await clothesShortController.GetIncludesById(ClothesInformationDomainOk.Value.Id);

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
            var clothesInformationDomain = new ResultValue<IClothesInformationDomain>(initialError);
            var clothesShortDatabaseService = GetClothesDatabaseService(ClothesShortDomainsOk, clothesInformationDomain);
            var clothesShortController = new ClothesController(clothesShortDatabaseService.Object,
                                                               ClothesShortTransferConverter,
                                                               ClothesInformationTransferConverter);

            var actionResult = await clothesShortController.GetIncludesById(ClothesInformationDomainOk.Value.Id);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesShortDomain> clothesShortDomains,
                                                                               IResultValue<IClothesInformationDomain> clothesInformationDomain) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetClothesShorts(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesShortDomains)).
            Void(mock => mock.Setup(service => service.GetIncludesById(It.IsAny<int>())).ReturnsAsync(clothesInformationDomain));

        /// <summary>
        /// Данные одежды. Корректный вариант
        /// </summary>
        private static ResultCollection<IClothesShortDomain> ClothesShortDomainsOk =>
            new ResultCollection<IClothesShortDomain>(ClothesData.ClothesShortDomains);

        /// <summary>
        /// Данные информации об одежде. Корректный вариант
        /// </summary>
        private static ResultValue<IClothesInformationDomain> ClothesInformationDomainOk =>
            new ResultValue<IClothesInformationDomain>(ClothesData.ClothesInformationDomains.First());

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        private static IClothesShortTransferConverter ClothesShortTransferConverter =>
            new ClothesShortTransferConverter();

        /// <summary>
        /// Конвертер информации об одежде в трансферную модель
        /// </summary>
        private static IClothesInformationTransferConverter ClothesInformationTransferConverter =>
            new ClothesInformationTransferConverter(new ClothesShortTransferConverter(), 
                                                    new GenderTransferConverter(), 
                                                    new ClothesTypeTransferConverter(new CategoryTransferConverter()), 
                                                    new ColorClothesTransferConverter(), 
                                                    new SizeGroupTransferConverter(new SizeTransferConverter()));
    }
}