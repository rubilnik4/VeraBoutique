using System.Security.Claims;

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
        public static string? GetEmail(ClaimsPrincipal user) =>
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}