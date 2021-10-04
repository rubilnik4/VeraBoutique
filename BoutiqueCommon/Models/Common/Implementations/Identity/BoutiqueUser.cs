using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;

namespace BoutiqueCommon.Models.Common.Implementations.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class BoutiqueUser : IBoutiqueUser
    {
        public BoutiqueUser(string userName, string email, IdentityRoleType identityRoleType,
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
    }
}