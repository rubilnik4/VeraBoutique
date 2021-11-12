using System;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identities
{
    /// <summary>
    /// Пользователь. Доменная модель
    /// </summary>
    public interface IBoutiqueUserDomain : IBoutiqueUserBase, IPersonalDomain
    {
        /// <summary>
        /// Обновить личную информацию
        /// </summary>
        IBoutiqueUserDomain UpdatePersonal(IPersonalDomain personal);

        /// <summary>
        /// Обновить личную информацию
        /// </summary>
        IBoutiqueUserDomain UpdatePersonal(string name, string surname, string address, string phone);
    }
}