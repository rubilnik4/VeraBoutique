using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems
{
    /// <summary>
    /// Ошибки личный данных
    /// </summary>
    public static class RegisterPersonalError
    {
        /// <summary>
        /// Получить ошибку авторизации
        /// </summary>
        public static IEnumerable<IErrorResult> GetResult<TValue>(TValue value, bool isValid,string description)
            where TValue : notnull =>
            isValid.ToResultValueWhere(valid => valid,
                                       _ => ErrorResultFactory.ValueNotValidError(value, typeof(RegisterPersonalError), description)).
                    Errors;
    }
}