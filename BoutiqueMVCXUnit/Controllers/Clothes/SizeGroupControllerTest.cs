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
    /// Контроллер для получения и записи группы размеров одежды
    /// </summary>
    public class SizeGroupControllerTest
    {
        /// <summary>
        /// Получить группу размеров одежды совместно с размерами. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetSizeGroupsIncludeSize_Ok()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var sizeGroupDomain = new ResultValue<ISizeGroupDomain>(sizeGroupInitial);
            var sizeGroupDatabaseService = GetSizeGroupDatabaseService(sizeGroupDomain);
            var sizeGroupTransferConverter = SizeGroupTransferConverter;
            var sizeGroupController = new SizeGroupController(sizeGroupDatabaseService.Object, sizeGroupTransferConverter);

            var sizeGroupTransfer = await sizeGroupController.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);
            var sizeGroupAfter = sizeGroupTransferConverter.FromTransfer(sizeGroupTransfer.Value);

            Assert.True(sizeGroupAfter.Equals(sizeGroupDomain.Value));
        }


        /// <summary>
        /// Получить группу размеров одежды совместно с размерами. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetByGender_ErrorDatabase()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var initialError = ErrorData.DatabaseError;
            var sizeGroupDomains = new  ResultValue<ISizeGroupDomain>(initialError);
            var sizeGroupDatabaseService = GetSizeGroupDatabaseService(sizeGroupDomains);
            var sizeGroupTransferConverter = SizeGroupTransferConverter;
            var sizeGroupController = new SizeGroupController(sizeGroupDatabaseService.Object, sizeGroupTransferConverter);

            var actionResult = await sizeGroupController.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

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
        public async Task GetByGender_NotFound()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            var initialError = ErrorData.NotFoundError;
            var sizeGroupDomains = new ResultValue<ISizeGroupDomain>(initialError);
            var sizeGroupDatabaseService = GetSizeGroupDatabaseService(sizeGroupDomains);
            var sizeGroupTransferConverter = SizeGroupTransferConverter;
            var sizeGroupController = new SizeGroupController(sizeGroupDatabaseService.Object, sizeGroupTransferConverter);

            var actionResult = await sizeGroupController.GetSizeGroupIncludeSize(clothesSizeType, sizeNormalize);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private static Mock<ISizeGroupDatabaseService> GetSizeGroupDatabaseService(IResultValue<ISizeGroupDomain> sizeGroupDomain) =>
            new Mock<ISizeGroupDatabaseService>().
            Void(mock => mock.Setup(service => service.GetSizeGroupIncludeSize(It.IsAny<ClothesSizeType>(), It.IsAny<int>())).
                              ReturnsAsync(sizeGroupDomain));

        /// <summary>
        /// Конвертер группы размеров одежды в трансферную модель
        /// </summary>
        private static ISizeGroupTransferConverter SizeGroupTransferConverter =>
            new SizeGroupTransferConverter(new SizeTransferConverter());
    }
}