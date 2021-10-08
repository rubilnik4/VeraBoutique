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
    /// Проверка текста
    /// </summary>
    public static class TextValidation
    {
        /// <summary>
        /// Проверить пароль на корректность
        /// </summary>
        public static bool IsValid(string text) =>
            text.
            ToResultValueWhere(textValue => !String.IsNullOrWhiteSpace(textValue), GetValidationError).
            ResultValueBindOk(OnlyLettersTry).
            Map(result => result.OkStatus && result.Value);

        /// <summary>
        /// Проверка текста на символы
        /// </summary>
        private static IResultValue<bool> OnlyLettersTry(string text) =>
             ResultValueTryExtensions.ResultValueTry(() => OnlyLetters(text), GetValidationError(text));

        /// <summary>
        /// Проверка текста на символы
        /// </summary>
        private static bool OnlyLetters(string text) =>
            Regex.IsMatch(text, @"^[a-zA-Zа-яА-Я]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Получить ошибку проверки
        /// </summary>
        private static IErrorResult GetValidationError(string text) =>
            ErrorResultFactory.ValueNotValidError(text, typeof(TextValidation), String.Empty);
    }
}