using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
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
                : GetIdentityError(identityResult.Errors.First().Code).
                  ToResultValue<TId>();

        private static IErrorResult GetIdentityError(string codeType) =>
            codeType switch
            {
                "DuplicateUserName" => ErrorResultFactory.ValueDuplicatedError(codeType, typeof(IdentityToResultValue), "Дублирование имени пользователя"),
                "DuplicateEmail" => ErrorResultFactory.ValueDuplicatedError(codeType, typeof(IdentityToResultValue), "Дублирование почты пользователя"),
                "DuplicateRoleName" => ErrorResultFactory.ValueDuplicatedError(codeType, typeof(IdentityToResultValue), "Дублирование роли"),
                _ => ErrorResultFactory.ValueNotValidError(codeType, typeof(IdentityToResultValue), "Ошибка идентификации"),
            };
    }
}