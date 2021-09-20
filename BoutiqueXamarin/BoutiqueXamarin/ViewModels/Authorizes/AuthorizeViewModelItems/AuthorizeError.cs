using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems
{
    /// <summary>
    /// Ошибки авторизации
    /// </summary>
    public static class AuthorizeError
    {
        /// <summary>
        /// Получить ошибку авторизации
        /// </summary>
        public static IEnumerable<IErrorResult> GetResult(bool isValid, AuthorizeErrorType authorizeErrorType, string description) =>
            isValid.ToResultValueWhere(valid => valid,
                                       _ => ErrorResultFactory.AuthorizeError(authorizeErrorType, description)).
                    Errors;
    }
}