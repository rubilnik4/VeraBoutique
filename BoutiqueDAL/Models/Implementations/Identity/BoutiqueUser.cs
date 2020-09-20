using System;
using BoutiqueDAL.Models.Enums.Identity;
using Functional.FunctionalExtensions.Sync;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public sealed class BoutiqueUser: IdentityUser
    {
        public BoutiqueUser(IdentityRoleType identityRoleType, IdentityLogin login, string email, string phone)
            :this(identityRoleType, login.UserName, login.Password, email, phone)
        { }

        public BoutiqueUser(IdentityRoleType identityRoleType, string userName, string password, string email, string phone)
        {
            IdentityRoleType = identityRoleType;
            UserName = userName;
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
        public override string NormalizedUserName => UserName.ToUpperInvariant();

        /// <summary>
        /// Нормализованная почта
        /// </summary>
        public override string NormalizedEmail => Email.ToUpperInvariant();

        /// <summary>
        /// Подтверждена ли почта
        /// </summary>
        public override bool EmailConfirmed =>  true;

        /// <summary>
        /// Подтвержден ли телефон
        /// </summary>
        public override bool PhoneNumberConfirmed => true;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override string SecurityStamp => Guid.NewGuid().ToString();

        /// <summary>
        /// Хэшировать пароль
        /// </summary>
        private static string GetPasswordHash(IdentityUser user, string password) =>
            new PasswordHasher<IdentityUser>().
            Map(passwordHasher => passwordHasher.HashPassword(user, password));
    }
}