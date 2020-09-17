using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace BoutiqueMVC.Models.Implementations.Authentication
{
    /// <summary>
    /// Параметры JWT токена
    /// </summary>
    public class JwtSettings
    {
        public JwtSettings(string issuer, string audience, byte[] key)
        {
            Key = key;
            Issuer = issuer;
            Audience = audience;
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
        /// Ключ
        /// </summary>
        public byte[] Key { get; }

        /// <summary>
        /// Параметры токена авторизации
        /// </summary>
        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
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