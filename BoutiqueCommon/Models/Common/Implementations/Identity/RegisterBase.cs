using System;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public abstract class RegisterBase<TAuthorize, TPersonal> : IRegisterBase<TAuthorize, TPersonal>
        where TAuthorize : IAuthorizeBase
        where TPersonal : IPersonalBase
    {
        protected RegisterBase(TAuthorize authorize, TPersonal personal)
        {
            Authorize = authorize;
            Personal = personal;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Authorize.Id;

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        public TAuthorize Authorize { get; }

        /// <summary>
        /// Личная информация
        /// </summary>
        public TPersonal Personal { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
          obj is IRegisterBase<TAuthorize, TPersonal> register && Equals(register);

        public bool Equals(IRegisterBase<TAuthorize, TPersonal>? other) =>
            other?.Authorize.Equals(Authorize) == true &&
            other?.Personal.Equals(Personal) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Authorize.GetHashCode(), Personal.GetHashCode());
        #endregion
    }
}