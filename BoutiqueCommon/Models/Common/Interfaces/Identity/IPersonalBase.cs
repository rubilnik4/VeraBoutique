using System;

namespace BoutiqueCommon.Models.Common.Interfaces.Identity
{
    /// <summary>
    /// Личная информация
    /// </summary>
    public interface IPersonalBase: IEquatable<IPersonalBase>
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