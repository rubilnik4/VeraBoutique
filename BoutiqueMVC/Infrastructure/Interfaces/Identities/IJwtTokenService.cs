using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;

namespace BoutiqueMVC.Infrastructure.Interfaces.Identities
{
    /// <summary>
    /// Сервис управления токенами
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Проверка токена
        /// </summary>
        bool IsTokenValid(string token);

        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        string GenerateJwtToken(IBoutiqueUserDomain user);
    }
}