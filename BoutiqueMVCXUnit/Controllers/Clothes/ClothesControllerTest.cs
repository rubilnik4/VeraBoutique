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
            var clothesController = new ClothesController(clothesDatabaseService.Object, clothesTransferConverter, 
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter);

            var clothesTransfers = await clothesController.GetClothes(genderType, clothesType);
            var clothesAfter = clothesTransferConverter.FromTransfers(clothesTransfers.Value);

            Assert.True(clothesAfter.Value.SequenceEqual(clothesDomains));
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
                                                          ClothesTransferConverterMock.ClothesTransferConverter,
                                                          ClothesTransferConverterMock.ClothesMainTransferConverter);

            var actionResult = await clothesController.GetClothes(genderType, clothesType);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
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
    }
}