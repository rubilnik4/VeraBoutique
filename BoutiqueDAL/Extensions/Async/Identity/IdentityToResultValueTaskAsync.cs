using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Extensions.Sync.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Extensions.Async.Identity
{
    /// <summary>
    /// Преобразование ответа авторизации в результирующий ответ
    /// </summary>
    public static class IdentityToResultValueTaskAsync
    {
        /// <summary>
        /// Преобразовать ответ авторизации в результирующий ответ
        /// </summary>
        public static async Task<IResultValue<TId>> ToIdentityResultValueTaskAsync<TId>(this Task<IdentityResult> identityResult, TId id)
            where TId : notnull =>
            await identityResult.
            MapTaskAsync(awaitedIdentity => awaitedIdentity.ToIdentityResultValue(id));
    }
}