using System.Linq;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Extensions.Sync.Identity
{
    /// <summary>
    /// Преобразование ответа авторизации в результирующий ответ
    /// </summary>
    public static class IdentityToResultValue
    {
        /// <summary>
        /// Преобразовать ответ авторизации в результирующий ответ
        /// </summary>
        public static IResultValue<TId> ToIdentityResultValue<TId>(this IdentityResult identityResult, TId id)
            where TId : notnull =>
            identityResult.Succeeded
                ? new ResultValue<TId>(id)
                : ErrorResultFactory.SimpleErrorType(identityResult.Errors.First().Description).
                  ToResultValue<TId>();
    }
}