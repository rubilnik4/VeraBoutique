using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Common.Interfaces.Identities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public interface IBoutiqueUserBase : IPersonalBase, IEquatable<IBoutiqueUserBase>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Почта
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Роль
        /// </summary>
        IdentityRoleType IdentityRoleType { get; }
    }
}