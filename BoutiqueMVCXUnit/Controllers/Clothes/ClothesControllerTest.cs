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
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
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
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains);
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
            var clothesShortDatabaseService = GetClothesDatabaseService(clothesShortDomains);
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
        /// Сервис одежды в базе данных
        /// </summary>
        private static Mock<IClothesDatabaseService> GetClothesDatabaseService(IResultCollection<IClothesShortDomain> clothesShortDomains) =>
            new Mock<IClothesDatabaseService>().
            Void(mock => mock.Setup(service => service.GetClothesShorts(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesShortDomains));

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