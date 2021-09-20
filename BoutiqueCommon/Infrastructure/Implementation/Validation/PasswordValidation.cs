using System;
using System.Text.RegularExpressions;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation
{
    /// <summary>
    /// Проверка пароля
    /// </summary>
    public static class PasswordValidation
    {
        /// <summary>
        /// Проверить пароль на корректность
        /// </summary>
        public static bool IsValid(string password, int minLength, bool needDigit) =>
            password.
            ToResultValueWhere(passwordValue => !String.IsNullOrWhiteSpace(passwordValue), GetValidationError).
            ResultValueContinue(passwordValue => passwordValue.Length >= minLength,
                                passwordValue => passwordValue,
                                GetValidationError).
            ResultValueBindWhere(_ => needDigit,
                                 HasDigitTry,
                                 _ => new ResultValue<bool>(true)).
            Map(result => result.OkStatus && result.Value);

        /// <summary>
        /// Проверка пароля на цифры
        /// </summary>
        private static IResultValue<bool> HasDigitTry(string password) =>
             ResultValueTryExtensions.ResultValueTry(() => HasDigit(password), GetValidationError(password));

        /// <summary>
        /// Проверка пароля на цифры
        /// </summary>
        private static bool HasDigit(string password) =>
            Regex.IsMatch(password, @"\d+", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Получить ошибку проверки
        /// </summary>
        private static IErrorResult GetValidationError(string password) =>
            ErrorResultFactory.ValueNotValidError(password, typeof(PasswordValidation), String.Empty);
    }
}