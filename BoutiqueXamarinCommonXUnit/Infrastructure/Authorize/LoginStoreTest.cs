﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize;
using Functional.Models.Enums;
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
            Assert.Equal(ErrorResultType.ValueNotValid, saveResult.Errors.First().ErrorResultType);
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
            Assert.Equal(ErrorResultType.ValueNotValid, getResult.Errors.First().ErrorResultType);
        }
    }
}