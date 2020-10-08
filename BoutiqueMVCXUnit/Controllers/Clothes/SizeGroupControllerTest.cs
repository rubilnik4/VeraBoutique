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
            var sizeGroupDomains = new ResultCollection<ISizeGroupDomain>(SizeGroupData.GetSizeGroupDomain());
            var sizeGroupDatabaseService = GetSizeGroupDatabaseService(sizeGroupDomains);
            var sizeGroupTransferConverter = SizeGroupTransferConverter;
            var sizeGroupController = new SizeGroupController(sizeGroupDatabaseService.Object, sizeGroupTransferConverter);

            var sizeGroupTransfers = await sizeGroupController.GetSizeGroupsIncludeSize(clothesSizeType, sizeNormalize);
            var sizeGroupAfter = sizeGroupTransferConverter.FromTransfers(sizeGroupTransfers.Value);

            Assert.True(sizeGroupAfter.SequenceEqual(sizeGroupDomains.Value));
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
            var sizeGroupDomains = new ResultCollection<ISizeGroupDomain>(initialError);
            var sizeGroupDatabaseService = GetSizeGroupDatabaseService(sizeGroupDomains);
            var sizeGroupTransferConverter = SizeGroupTransferConverter;
            var sizeGroupController = new SizeGroupController(sizeGroupDatabaseService.Object, sizeGroupTransferConverter);

            var actionResult = await sizeGroupController.GetSizeGroupsIncludeSize(clothesSizeType, sizeNormalize);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private static Mock<ISizeGroupDatabaseService> GetSizeGroupDatabaseService(IResultCollection<ISizeGroupDomain> sizeGroupDomains) =>
            new Mock<ISizeGroupDatabaseService>().
            Void(mock => mock.Setup(service => service.GetSizeGroupsIncludeSize(It.IsAny<ClothesSizeType>(), It.IsAny<int>())).
                              ReturnsAsync(sizeGroupDomains));

        /// <summary>
        /// Конвертер группы размеров одежды в трансферную модель
        /// </summary>
        private static ISizeGroupTransferConverter SizeGroupTransferConverter =>
            new SizeGroupTransferConverter(new SizeTransferConverter());
    }
}