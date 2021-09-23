using System;
using BoutiqueCommon.Models.Common.Interfaces.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Личная информация
    /// </summary>
    public abstract class PersonalBase : IPersonalBase
    {
        protected PersonalBase(string name, string surname, string address, string phone)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Phone = phone;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => $"{Name} {Surname}";

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
        public string Phone { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is IPersonalBase personal && Equals(personal);

        public bool Equals(IPersonalBase? other) =>
            other?.Name == Name &&
            other?.Surname == Surname &&
            other?.Phone == Phone;

        public override int GetHashCode() =>
            HashCode.Combine(Name, Surname, Phone);
        #endregion
    }
}