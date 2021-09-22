using System;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public sealed class BoutiqueUser: IdentityUser
    {
        public BoutiqueUser(IdentityRoleType identityRoleType, string email, string password,  string phone)
        {
            IdentityRoleType = identityRoleType;
            UserName = email;
            Email = email;
            PhoneNumber = phone;
            PasswordHash = GetPasswordHash(this, password);
        }

        /// <summary>
        /// Тип роли
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }

        /// <summary>
        /// Нормализованное имя пользователя
        /// </summary>
        public override string NormalizedUserName => 
            UserName.ToUpperInvariant();

        /// <summary>
        /// Нормализованная почта
        /// </summary>
        public override string NormalizedEmail => 
            Email.ToUpperInvariant();

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override string SecurityStamp => 
            Guid.NewGuid().ToString();

        /// <summary>
        /// Хэшировать пароль
        /// </summary>
        private static string GetPasswordHash(IdentityUser user, string password) =>
            new PasswordHasher<IdentityUser>().
            Map(passwordHasher => passwordHasher.HashPassword(user, password));
    }
}