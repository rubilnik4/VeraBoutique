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
    public sealed class BoutiqueUser : IdentityUser
    {
        public BoutiqueUser()
        { }

        public BoutiqueUser(string email, string name, string surname, string address, string phoneNumber)
            : this(email, null, name, surname, address, phoneNumber)
        { }

        public BoutiqueUser(string email, string? password, string name, string surname, string address, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            Address = address;
            UserName = email;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = GetPasswordHash(this, password);
        }

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
        /// Имя
        /// </summary>
        public string Name { get; init; } = String.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; init; } = String.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; init; } = String.Empty;

        /// <summary>
        /// Хэшировать пароль
        /// </summary>
        private static string GetPasswordHash(IdentityUser user, string? password) =>
            new PasswordHasher<IdentityUser>().
            Map(passwordHasher => passwordHasher.HashPassword(user, password));

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public static BoutiqueUser GetBoutiqueUser(IRegisterDomain register) =>
            new(register.Authorize.Email, register.Authorize.Password, register.Personal.Name,
                register.Personal.Surname, register.Personal.Address, register.Personal.Phone);
    }
}