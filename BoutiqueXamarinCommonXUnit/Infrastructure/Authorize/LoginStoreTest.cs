using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Authorize
{
    /// <summary>
    /// Хранение и загрузка данных аутентификации. Тесты
    /// </summary>
    public class LoginStoreTest
    {
        /// <summary>
        /// Сохранение и получение токена
        /// </summary>
        [Fact]
        public async Task SaveAndGetToken()
        {
            const string token = "token";
            var loginStore = new LoginStore();
            var saveResult = await loginStore.SaveToken(token);
            var getResult = await loginStore.GetToken();
            await loginStore.ClearToken();

            Assert.True(saveResult.OkStatus);
            Assert.True(getResult.OkStatus);
            Assert.Equal(token, getResult.Value);
        }

        /// <summary>
        /// Сохранение токена дважды
        /// </summary>
        [Fact]
        public async Task DoubleSave()
        {
            const string tokenFirst = "tokenFirst";
            const string tokenSecond = "tokenFirst";
            var loginStore = new LoginStore();
            var saveResultFirst = await loginStore.SaveToken(tokenFirst);
            var saveResultSecond = await loginStore.SaveToken(tokenSecond);
            var getResult = await loginStore.GetToken();
            await loginStore.ClearToken();

            Assert.True(saveResultFirst.OkStatus);
            Assert.True(saveResultSecond.OkStatus);
            Assert.Equal(tokenSecond, getResult.Value);
        }

        /// <summary>
        /// Сохранение токена. Ошибка
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task SaveToken_Error(string token)
        {
            var loginStore = new LoginStore();
            var saveResult = await loginStore.SaveToken(token);
            await loginStore.ClearToken();

            Assert.True(saveResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(saveResult.Errors.First());
        }

        /// <summary>
        /// Загрузка токена. Ошибка
        /// </summary>
        [Fact]
        public async Task GetToken_Error()
        {
            var loginStore = new LoginStore();
            await loginStore.ClearToken();

            var getResult = await loginStore.GetToken();

            Assert.True(getResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(getResult.Errors.First());
        }
    }
}