using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identities
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public interface IRegisterBase<TAuthorize, TPersonal> : IModel<string>, IEquatable<IRegisterBase<TAuthorize, TPersonal>>
        where TAuthorize: IAuthorizeBase
        where TPersonal: IPersonalBase 
    {
        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        TAuthorize Authorize { get; }

        /// <summary>
        /// Личная информация
        /// </summary>
        TPersonal Personal { get; }
    }
}