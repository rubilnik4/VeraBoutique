using System;
using System.Text.RegularExpressions;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation.Common
{
    /// <summary>
    /// Проверка телефона
    /// </summary>
    public static class PhoneValidation
    {
        /// <summary>
        /// Проверить пароль на корректность
        /// </summary>
        public static bool IsValid(string phone) =>
            phone.
            ToResultValueWhere(textValue => !String.IsNullOrWhiteSpace(textValue), GetValidationError).
            ResultValueBindOk(PhoneMatchTry).
            Map(result => result.OkStatus && result.Value);

        /// <summary>
        /// Получить номер телефона без кода страны
        /// </summary>
        public static string GetPhoneWithoutCountry(string phone) =>
            Regex.Replace(phone, @"^(\+7|7|8)", "");

        /// <summary>
        /// Проверка текста на символы
        /// </summary>
        private static IResultValue<bool> PhoneMatchTry(string text) =>
             ResultValueTryExtensions.ResultValueTry(() => PhoneMatch(text), GetValidationError(text));

        /// <summary>
        /// Проверка текста на символы
        /// </summary>
        private static bool PhoneMatch(string phone) =>
            Regex.IsMatch(phone, @" ^ (\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Получить ошибку проверки
        /// </summary>
        private static IErrorResult GetValidationError(string phone) =>
            ErrorResultFactory.ValueNotValidError(phone, typeof(PhoneValidation), String.Empty);
    }
}