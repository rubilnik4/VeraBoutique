using System.Collections.Generic;
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
    /// Контроллер для получения и записи вида одежды
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
            string category = CategoryData.GetCategoryDomain().First().Name;
            var clothesTypeDomains = new ResultCollection<IClothesTypeDomain>(ClothesTypeData.GetClothesTypeDomain());
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeTransferConverter = ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object, clothesTypeTransferConverter);

            var clothesTypeTransfers = await clothesTypeController.GetByGenderCategory(genderType, category);
            var clothesTypeAfter = clothesTypeTransferConverter.FromTransfers(clothesTypeTransfers.Value);

            Assert.True(clothesTypeAfter.SequenceEqual(clothesTypeDomains.Value));
        }

        /// <summary>
        /// Получить вид одежды по типу пола. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            const GenderType genderType = GenderType.Male;
            string category = CategoryData.GetCategoryDomain().First().Name;
            var initialError = ErrorData.DatabaseError;
            var clothesTypeDomains = new ResultCollection<IClothesTypeDomain>(initialError);
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeTransferConverter = ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object, clothesTypeTransferConverter);

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
            string category = CategoryData.GetCategoryDomain().First().Name;
            var initialError = ErrorData.NotFoundError;
            var clothesTypeDomains = new ResultCollection<IClothesTypeDomain>(initialError);
            var clothesTypeDatabaseService = GetClothesTypeDatabaseService(clothesTypeDomains);
            var clothesTypeTransferConverter = ClothesTypeTransferConverter;
            var clothesTypeController = new ClothesTypeController(clothesTypeDatabaseService.Object, clothesTypeTransferConverter);

            var actionResult = await clothesTypeController.GetByGenderCategory(genderType, category);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private static Mock<IClothesTypeDatabaseService> GetClothesTypeDatabaseService(IResultCollection<IClothesTypeDomain> clothesTypeDomains) =>
            new Mock<IClothesTypeDatabaseService>().
            Void(mock => mock.Setup(service => service.GetByGenderCategory(It.IsAny<GenderType>(), It.IsAny<string>())).
                              ReturnsAsync(clothesTypeDomains));

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private static IClothesTypeTransferConverter ClothesTypeTransferConverter => 
            new ClothesTypeTransferConverter();
    }
}