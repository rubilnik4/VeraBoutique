using System;
using System.Linq;
using BoutiqueDAL.Extensions.Sync.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDALXUnit.Extensions.Sync.Identity
{
    /// <summary>
    /// Преобразование ответа авторизации в результирующий ответ. Тесты
    /// </summary>
    public class IdentityToResultValueTest
    {
        /// <summary>
        /// Успешное преобразование
        /// </summary>
        [Fact]
        public void ToResultValue_Success()
        {
            const string id = "id";
            var identity = IdentityResult.Success;

            var result = identity.ToIdentityResultValue(id);

            Assert.True(result.OkStatus);
            Assert.Equal(id, result.Value);
        }

        /// <summary>
        /// Преобразование с ошибкой
        /// </summary>
        [Fact]
        public void ToResultValue_Error()
        {
            var identityError = new IdentityError {Code = "Error", Description = "Error"};
            var identity = IdentityResult.Failed(identityError);

            var result = identity.ToIdentityResultValue(String.Empty);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Преобразование с ошибкой
        /// </summary>
        [Fact]
        public void ToResultValue_Duplicate()
        {
            var identityError = new IdentityError { Code = "DuplicateUserName", Description = "DuplicateUserName" };
            var identity = IdentityResult.Failed(identityError);

            var result = identity.ToIdentityResultValue(String.Empty);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueDuplicatedErrorResult>(result.Errors.First());
        }
    }
}