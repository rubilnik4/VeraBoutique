using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Extensions.Async.Identity;
using BoutiqueDAL.Extensions.Sync.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDALXUnit.Extensions.Async.Identity
{
    /// <summary>
    /// Преобразование ответа авторизации в результирующий ответ. Тесты
    /// </summary>
    public class IdentityToResultValueTaskAsync
    {
        /// <summary>
        /// Успешное преобразование
        /// </summary>
        [Fact]
        public async Task ToResultValue_Success()
        {
            const string id = "id";
            var identity = Task.FromResult(IdentityResult.Success);

            var result = await identity.ToIdentityResultValueTaskAsync(id);

            Assert.True(result.OkStatus);
            Assert.Equal(id, result.Value);
        }

        /// <summary>
        /// Преобразование с ошибкой
        /// </summary>
        [Fact]
        public async Task ToResultValue_Error()
        {
            var identityError = new IdentityError { Description = "Error" };
            var identity = Task.FromResult(IdentityResult.Failed(identityError));

            var result = await identity.ToIdentityResultValueTaskAsync(String.Empty);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(result.Errors.First());
            Assert.Equal(identityError.Description, result.Errors.First().Description);
        }
    }
}