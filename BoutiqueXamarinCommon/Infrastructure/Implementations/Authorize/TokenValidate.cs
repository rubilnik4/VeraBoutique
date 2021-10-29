using System;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Authorize
{
    /// <summary>
    /// Проверка токена
    /// </summary>
    public static class TokenValidate
    {
        /// <summary>
        /// Проверка корректности токена
        /// </summary>
        public static bool IsTokenValid(string? token) =>
            !String.IsNullOrWhiteSpace(token);
    }
}