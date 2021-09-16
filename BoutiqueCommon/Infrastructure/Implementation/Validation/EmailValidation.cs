using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation
{
    /// <summary>
    /// Проверка корректности почты
    /// </summary>
    public static class EmailValidation
    {
        /// <summary>
        /// Проверить почту на корректность написания
        /// </summary>
        public static bool IsValidEmail(string email) =>
            email.
            ToResultValueWhere(emailValue => !String.IsNullOrWhiteSpace(emailValue),
                               GetValidationError).
            ResultValueBindOk(DomainValidationTry).
            ResultValueBindOk(IsMatchTry).
            Map(result => result.OkStatus && result.Value);

        /// <summary>
        /// Проверка доменного имени
        /// </summary>
        private static IResultValue<string> DomainValidationTry(string email) =>
            ResultValueTryExtensions.ResultValueTry(() => DomainValidation(email), GetValidationError(email));

        /// <summary>
        /// Проверка доменного имени
        /// </summary>
        private static string DomainValidation(string email) =>
            Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

        /// <summary>
        /// Проверка почты
        /// </summary>
        private static IResultValue<bool> IsMatchTry(string email) =>
             ResultValueTryExtensions.ResultValueTry(() => IsMatch(email), GetValidationError(email));

        /// <summary>
        /// Проверка почты
        /// </summary>
        private static bool IsMatch(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        /// <summary>
        /// Корректировка доменного имени
        /// </summary>
        private static string DomainMapper(Match match) =>
            new IdnMapping().
            Map(idn => idn.GetAscii(match.Groups[2].Value)).
            Map(domainName => match.Groups[1].Value + domainName);

        /// <summary>
        /// Получить ошибку проверки
        /// </summary>
        private static IErrorResult GetValidationError(string email) =>
            ErrorResultFactory.ValueNotValidError(email, typeof(EmailValidation), String.Empty);

        //{
        //    if (String.IsNullOrWhiteSpace(email)) return false;

        //    try
        //    {
        //        // Normalize the domain
        //        email = ;

        //        // Examines the domain part of the email and normalizes it.
        //        string DomainMapper(Match match)
        //        {
        //            // Use IdnMapping class to convert Unicode domain names.
        //            var idn = new IdnMapping();

        //            // Pull out and process domain name (throws ArgumentException on invalid)
        //            string domainName = idn.GetAscii(match.Groups[2].Value);

        //            return match.Groups[1].Value + domainName;
        //        }
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return false;
        //    }
        //    catch (ArgumentException)
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        //    }
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return false;
        //    }
        //}
    }
}