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
            var saveResult = await LoginStore.SaveToken(token);
            var getResult = await LoginStore.GetToken();
            await LoginStore.ClearToken();

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
            var saveResultFirst = await LoginStore.SaveToken(tokenFirst);
            var saveResultSecond = await LoginStore.SaveToken(tokenSecond);
            var getResult = await LoginStore.GetToken();
            await LoginStore.ClearToken();

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
            var saveResult = await LoginStore.SaveToken(token);
            await LoginStore.ClearToken();

            Assert.True(saveResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(saveResult.Errors.First());
        }

        /// <summary>
        /// Загрузка токена. Ошибка
        /// </summary>
        [Fact]
        public async Task GetToken_Error()
        {
            await LoginStore.ClearToken();

            var getResult = await LoginStore.GetToken();

            Assert.True(getResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(getResult.Errors.First());
        }
    }
}