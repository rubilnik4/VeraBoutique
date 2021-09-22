using Microsoft.IdentityModel.Tokens;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    /// <summary>
    /// Параметры JWT токена
    /// </summary>
    public class JwtSettings
    {
        public JwtSettings(string issuer, string audience, int expires, byte[] key)
        {
            Issuer = issuer;
            Audience = audience;
            Expires = expires;
            Key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// Срок действия токена
        /// </summary>
        public int Expires { get; }

        /// <summary>
        /// Ключ
        /// </summary>
        public byte[] Key { get; }

        /// <summary>
        /// Параметры токена авторизации
        /// </summary>
        public TokenValidationParameters TokenValidationParameters =>
            new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Key)
            };
    }
}