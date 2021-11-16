using System;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDAL.Models.Implementations.Entities.Identities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public sealed class BoutiqueUserEntity : IdentityUser
    {
        public BoutiqueUserEntity()
        { }

        public BoutiqueUserEntity(string email, string name, string surname, string address, string phoneNumber)
            : this(email, null, name, surname, address, phoneNumber)
        { }

        public BoutiqueUserEntity(string email, string? password, string name, string surname, string address, string phoneNumber)
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
        /// Имя
        /// </summary>
        public string Name { get; private set; } = String.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; private set; } = String.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; private set; } = String.Empty;

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public IBoutiqueUserDomain ToBoutiqueUser(IdentityRoleType identityRoleType) =>
            new BoutiqueUserDomain(UserName, Email, identityRoleType, Name, Surname, Address, PhoneNumber);

        /// <summary>
        /// Обновить личные данные
        /// </summary>
        public BoutiqueUserEntity UpdatePersonal(IPersonalDomain personal)
        {
            Name = personal.Name;
            Surname = personal.Surname;
            Address = personal.Address;
            PhoneNumber = personal.Phone;
            return this;
        }

        /// <summary>
        /// Хэшировать пароль
        /// </summary>
        private static string? GetPasswordHash(IdentityUser user, string? password) =>
            new PasswordHasher<IdentityUser>().
            Map(passwordHasher => password is not null ? passwordHasher.HashPassword(user, password) : null);

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public static BoutiqueUserEntity GetBoutiqueUser(IRegisterDomain register) =>
            new(register.Authorize.Email, register.Authorize.Password, register.Personal.Name,
                register.Personal.Surname, register.Personal.Address, register.Personal.Phone);

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public static BoutiqueUserEntity GetBoutiqueUser(IBoutiqueUserDomain user) =>
           new(user.Email, user.Name, user.Surname, user.Address, user.Phone);
    }
}