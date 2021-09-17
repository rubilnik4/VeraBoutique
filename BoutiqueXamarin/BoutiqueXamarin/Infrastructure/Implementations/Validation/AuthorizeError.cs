using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Infrastructure.Implementations.Validation
{
    /// <summary>
    /// Ошибки при проверке
    /// </summary>
    public static class AuthorizeError
    {
        /// <summary>
        /// Проверка по имени пользователя
        /// </summary>
        public static IResultError GetEmailError(string login, bool loginValid) =>
            login.ToResultValueWhere(_ => loginValid, _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Email,
                                                                                               "Почта указана некорректно"));

        /// <summary>
        /// Проверка пароля
        /// </summary>
        public static IResultError GetPasswordError(string password, bool passwordValid) =>
            password.ToResultValueWhere(_ => passwordValid, _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password,
                                                                                                     "Пароль указан некорректно"));
    }
}