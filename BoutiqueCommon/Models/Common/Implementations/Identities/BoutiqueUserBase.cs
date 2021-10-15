using System;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Common.Implementations.Identities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public abstract class BoutiqueUserBase : IBoutiqueUserBase
    {
        protected BoutiqueUserBase(string userName, string email, IdentityRoleType identityRoleType,
                                   string name, string surname, string address, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            IdentityRoleType = identityRoleType;
            Name = name;
            Surname = surname;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id =>
            UserName;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Роль
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string PhoneNumber { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is IBoutiqueUserBase user && Equals(user);

        public bool Equals(IBoutiqueUserBase? other) =>
            other?.Email == Email;

        public override int GetHashCode() =>
            HashCode.Combine(Email);
        #endregion
    }
}