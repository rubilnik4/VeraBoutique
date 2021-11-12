using System;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Common.Implementations.Identities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public abstract class BoutiqueUserBase : PersonalBase, IBoutiqueUserBase
    {
        protected BoutiqueUserBase(string userName, string email, IdentityRoleType identityRoleType,
                                   string name, string surname, string address, string phone)
            :base(name, surname, address, phone)
        {
            UserName = userName;
            Email = email;
            IdentityRoleType = identityRoleType;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override string Id =>
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