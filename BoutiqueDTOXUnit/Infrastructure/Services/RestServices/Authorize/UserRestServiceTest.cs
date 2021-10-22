using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
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
    public class UserRestServiceTest
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
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

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
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

            var resultId = await registerRestService.Register(register);

            Assert.True(resultId.HasErrors);
            Assert.IsType<RestMessageErrorResult>(resultId.Errors.First());
        }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        [Fact]
        public async Task GetUsers()
        {
            var users = IdentityData.BoutiqueUsers;
            var userTransfers = IdentityTransfersData.BoutiqueUserTransfers;
            var resultUsers = new ResultCollection<BoutiqueUserTransfer>(userTransfers);
            var restHttpClient = RestClientMock.GetRestClient(resultUsers);
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

            var result = await registerRestService.GetUsers();

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(users));
        }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        [Fact]
        public async Task GetUsers_Error()
        {
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultUsers = new ResultCollection<BoutiqueUserTransfer>(error);
            var restHttpClient = RestClientMock.GetRestClient(resultUsers);
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

            var result = await registerRestService.GetUsers();

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        [Fact]
        public async Task DeleteUsers()
        {
            var resultUsers = new ResultError();
            var restHttpClient = RestClientMock.DeleteRestClient(resultUsers);
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

            var result = await registerRestService.DeleteUsers();

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        [Fact]
        public async Task DeleteUsers_Error()
        {
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultUsers = new ResultError(error);
            var restHttpClient = RestClientMock.DeleteRestClient(resultUsers);
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;
            var userTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var registerRestService = new UserRestService(restHttpClient.Object, registerTransferConverter, userTransferConverter);

            var result = await registerRestService.DeleteUsers();

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
        }
    }
}