using System;
using BoutiqueDAL.Models.Implementations.Connection;
using BoutiqueDALXUnit.Data;
using Xunit;

namespace BoutiqueDALXUnit.Models.Connection
{
    /// <summary>
    /// Параметры авторизации. Тесты
    /// </summary>
    public class AuthorizationTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Equal_Ok()
        {
            const string username = "username";
            const string password = "password";

            var authorization = new Authorization(username, password);

            int authorizationHash = HashCode.Combine(username, password);
            Assert.Equal(authorizationHash, authorization.GetHashCode());
        }
    }
}