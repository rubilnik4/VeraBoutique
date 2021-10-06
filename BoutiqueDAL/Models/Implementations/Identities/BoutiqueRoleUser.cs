using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueDAL.Models.Implementations.Identities
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
        public IBoutiqueUserDomain ToBoutiqueUser() =>
            new BoutiqueUserDomain(BoutiqueIdentityUser.UserName, BoutiqueIdentityUser.Email, IdentityRoleType,
                             BoutiqueIdentityUser.Name, BoutiqueIdentityUser.Surname, BoutiqueIdentityUser.Address,
                             BoutiqueIdentityUser.PhoneNumber);

        /// <summary>
        /// Сравнение с пользователем
        /// </summary>
        public bool EqualToBoutiqueUser(IBoutiqueUserBase userBase) =>
            userBase.UserName == BoutiqueIdentityUser.UserName && userBase.Email == BoutiqueIdentityUser.Email &&
            userBase.IdentityRoleType == IdentityRoleType && userBase.Name == BoutiqueIdentityUser.Name &&
            userBase.Surname == BoutiqueIdentityUser.Surname && userBase.Address == BoutiqueIdentityUser.Address &&
            userBase.PhoneNumber == BoutiqueIdentityUser.PhoneNumber;
    }
}