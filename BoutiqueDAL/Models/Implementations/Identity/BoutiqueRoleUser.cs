using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Models.Enums.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Пользователь с ролью
    /// </summary>
    public class BoutiqueRoleUser
    {
        public BoutiqueRoleUser(IdentityRoleType identityRoleType, BoutiqueIdentityUser boutiqueIdentityUser)
        {
            IdentityRoleType = identityRoleType;
            BoutiqueIdentityUser = boutiqueIdentityUser;
        }

        /// <summary>
        /// Тип роли
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public BoutiqueIdentityUser BoutiqueIdentityUser { get; }

        /// <summary>
        /// Преобразовать в пользователя
        /// </summary>
        public IBoutiqueUser ToBoutiqueUser() =>
            new BoutiqueUser(BoutiqueIdentityUser.UserName, BoutiqueIdentityUser.Email, IdentityRoleType,
                             BoutiqueIdentityUser.Name, BoutiqueIdentityUser.Surname, BoutiqueIdentityUser.Address,
                             BoutiqueIdentityUser.PhoneNumber);

        /// <summary>
        /// Сравнение с пользователем
        /// </summary>
        public bool EqualToBoutiqueUser(IBoutiqueUser user) =>
            user.UserName == BoutiqueIdentityUser.UserName && user.Email == BoutiqueIdentityUser.Email &&
            user.IdentityRoleType == IdentityRoleType && user.Name == BoutiqueIdentityUser.Name &&
            user.Surname == BoutiqueIdentityUser.Surname && user.Address == BoutiqueIdentityUser.Address &&
            user.PhoneNumber == BoutiqueIdentityUser.PhoneNumber;
    }
}