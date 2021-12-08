using System.Security.Claims;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Информация из токена
    /// </summary>
    public static class ClaimsInformation
    {
        /// <summary>
        /// Получить адрес почты из токена
        /// </summary>
        public static IResultValue<string> GetEmail(ClaimsPrincipal user) =>
            user.FindFirst(ClaimTypes.NameIdentifier).
            ToResultValueNullCheck(ErrorResultFactory.ValueNotValidError(ClaimTypes.NameIdentifier, typeof(ClaimsInformation),
                                                                         "Почтовый адрес в токене не найден")).
            ResultValueOk(nameIdentifier => nameIdentifier.Value);
    }
}