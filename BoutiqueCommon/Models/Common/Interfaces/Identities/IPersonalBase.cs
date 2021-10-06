using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Identities
{
    /// <summary>
    /// Личная информация
    /// </summary>
    public interface IPersonalBase : IModel<string>, IEquatable<IPersonalBase>
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Телефон
        /// </summary>
        string Phone { get; }
    }
}