using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Implementations.Results;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Authorize
{
    /// <summary>
    /// Регистрация. Сервис. Тесты
    /// </summary>
    public class RegisterRestServiceTest
    {
        /// <summary>
        /// Зарегистрироваться в сервисе
        /// </summary>
        [Fact]
        public async Task Register_Ok()
        {
            const string userId = "userId";
            var register = RegisterData.RegisterDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(userId.ToResultValue());
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var registerRestService = new RegisterRestService(restHttpClient.Object, registerTransferConverter);

            var resultId = await registerRestService.Register(register);

            Assert.True(resultId.OkStatus);
            Assert.Equal(userId, resultId.Value);
        }

        /// <summary>
        /// Зарегистрироваться в сервисе. Ошибка
        /// </summary>
        [Fact]
        public async Task Register_Error()
        {
            var error = ErrorTransferData.ErrorTypeAuthorizeError;
            var userIdResult = new ResultValue<string>(error);
            var register = RegisterData.RegisterDomains.First();
            var restHttpClient = RestClientMock.PostRestClient(userIdResult);
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var registerRestService = new RegisterRestService(restHttpClient.Object, registerTransferConverter);

            var resultId = await registerRestService.Register(register);

            Assert.True(resultId.HasErrors);
            Assert.IsType<RestMessageErrorResult>(resultId.Errors.First());
        }
    }
}