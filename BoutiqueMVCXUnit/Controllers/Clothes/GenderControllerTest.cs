using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using BoutiqueMVC.Controllers.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
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
        [Fact]
        public async Task GetGenders_Ok()
        {
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.GetGenders()).
                              ReturnsAsync(new ResultCollection<Gender>(GetGenders()));
            var genderController = new GenderController(genderServiceMock.Object);

            var getGenders = await genderController.Get();
            var getGendersOk = (OkObjectResult)getGenders;

            Assert.NotNull(getGendersOk);
            Assert.Equal(200, getGendersOk.StatusCode);
        }

        [Fact]
        public async Task GetGenders_Bad()
        {
            var initialError = ErrorTest();
            var genderServiceMock = new Mock<IGenderService>();
            genderServiceMock.Setup(genderService => genderService.GetGenders()).
                              ReturnsAsync(new ResultCollection<Gender>(initialError));
            var genderController = new GenderController(genderServiceMock.Object);

            var getGenders = await genderController.Get();
            var getGendersBad = (BadRequestObjectResult)getGenders;
            var errors = (SerializableError)getGendersBad.Value;

            Assert.NotNull(getGendersBad);
            Assert.Equal(400, getGendersBad.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить типы пола
        /// </summary>
        private static IEnumerable<Gender> GetGenders() =>
            new List<Gender>()
            {
                new Gender(GenderType.Male, "Мужик" ),
                new Gender(GenderType.Female, "Тетя"),
            };

        /// <summary>
        /// Тестовая ошибка
        /// </summary>
        private static IErrorResult ErrorTest() =>
            new ErrorResult(ErrorResultType.Unknown, "Тестовая ошибка");
    }
}