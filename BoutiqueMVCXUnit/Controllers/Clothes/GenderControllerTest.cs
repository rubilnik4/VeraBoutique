using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола. Тесты
    /// </summary>
    public class GenderControllerTest
    {
        /// <summary>
        /// Получить типы пола одежды. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetGenders_Ok()
        {
            var genders = GetGenders().ToList();
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.Get()).
                              ReturnsAsync(new ResultCollection<IGenderDomain>(genders));
            var genderTransferConverter =(IGenderTransferConverter)new GenderTransferConverter();
            var genderController = new GenderController(genderServiceMock.Object, genderTransferConverter);

            var getGenders = await genderController.Get();
            var gendersAfter = genderTransferConverter.FromTransfers(getGenders.Value);

            Assert.IsType<OkObjectResult>(getGenders.Result);
            var getGendersOk = (OkObjectResult)getGenders.Result;
            Assert.Equal(StatusCodes.Status200OK, getGendersOk.StatusCode);
            Assert.True(gendersAfter.SequenceEqual(genders));
        }

        /// <summary>
        /// Получить типы пола одежды. Вариант с ошибкой
        /// </summary>
        [Fact]
        public async Task GetGenders_Bad()
        {
            var initialError = ErrorTest();
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.Get()).
                              ReturnsAsync(new ResultCollection<IGenderDomain>(initialError));
            var genderTransferConverter = (IGenderTransferConverter)new GenderTransferConverter();
            var genderController = new GenderController(genderServiceMock.Object, genderTransferConverter);

            var getGenders = await genderController.Get();

            Assert.IsType<BadRequestObjectResult>(getGenders.Result);
            var getGendersBad = (BadRequestObjectResult)getGenders.Result;
            var errors = (SerializableError)getGendersBad.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, getGendersBad.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Записать типы полов для одежды. Корректный вариант
        /// </summary>
        [Fact]
        public async Task PostGenders_Ok()
        {
            var genders = GetGenders().ToList();
            var genderTransferConverter = (IGenderTransferConverter)new GenderTransferConverter();
            var gendersDto = genderTransferConverter.ToTransfers(genders).ToList();
            var genderIds = genders.Select(gender => gender.GenderType).ToList();
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.Post(It.IsAny<IEnumerable<IGenderDomain>>())).
                              ReturnsAsync(new ResultCollection<GenderType>(genderIds));
          
            var genderController = new GenderController(genderServiceMock.Object, genderTransferConverter);
            var postGenders = await genderController.Post(gendersDto);

            Assert.IsType<CreatedAtActionResult>(postGenders.Result);
            var postGendersOk = (CreatedAtActionResult)postGenders.Result;
            Assert.Equal(StatusCodes.Status201Created, postGendersOk.StatusCode);
            Assert.Equal(nameof(GenderController), postGendersOk.ControllerName);
            Assert.True(genderIds.SequenceEqual((IEnumerable<GenderType>)postGendersOk.RouteValues.Values.First()));
        }

        /// <summary>
        /// Записать типы полов для одежды. Вариант с ошибкой
        /// </summary>
        [Fact]
        public async Task PostGenders_Bad()
        {
            var initialError = ErrorTest();
            var genders = GetGenders();
            var genderTransferConverter = (IGenderTransferConverter)new GenderTransferConverter();
            var gendersDto = genderTransferConverter.ToTransfers(genders).ToList();
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.Post(It.IsAny<IEnumerable<IGenderDomain>>())).
                              ReturnsAsync(new ResultCollection<GenderType>(initialError));

            var genderController = new GenderController(genderServiceMock.Object, genderTransferConverter);

            var postGenders = await genderController.Post(gendersDto);

            Assert.IsType<BadRequestObjectResult>(postGenders.Result);
            var postGendersBad = (BadRequestObjectResult)postGenders.Result;
            var errors = (SerializableError)postGendersBad.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, postGendersBad.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить типы пола
        /// </summary>
        private static IEnumerable<IGenderDomain> GetGenders() =>
            new List<IGenderDomain>()
            {
                new GenderDomain(GenderType.Male, "Мужик" ),
                new GenderDomain(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Тестовая ошибка
        /// </summary>
        private static IErrorResult ErrorTest() =>
            new ErrorResult(ErrorResultType.Unknown, "Тестовая ошибка");
    }
}