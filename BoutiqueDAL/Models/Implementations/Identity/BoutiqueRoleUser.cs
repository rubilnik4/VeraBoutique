using BoutiqueDAL.Models.Enums.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Пользователь с ролью
    /// </summary>
    public class BoutiqueRoleUser
    {
        public BoutiqueRoleUser(IdentityRoleType identityRoleType, BoutiqueUser boutiqueUser)
        {
            IdentityRoleType = identityRoleType;
            BoutiqueUser = boutiqueUser;
        }

        /// <summary>
        /// Тип роли
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public BoutiqueUser BoutiqueUser { get; }
    }
}