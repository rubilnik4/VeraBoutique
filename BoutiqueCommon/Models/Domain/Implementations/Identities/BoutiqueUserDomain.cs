using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Domain.Implementations.Identities
{
    /// <summary>
    /// Пользователь. Доменная модель
    /// </summary>
    public class BoutiqueUserDomain : BoutiqueUserBase, IBoutiqueUserDomain
    {
        public BoutiqueUserDomain(IBoutiqueUserBase boutiqueUser)
            :this(boutiqueUser.UserName, boutiqueUser.Email, boutiqueUser.IdentityRoleType,
                  boutiqueUser.Name, boutiqueUser.Surname, boutiqueUser.Address, boutiqueUser.Phone)
        { }

        public BoutiqueUserDomain(string userName, string email, IdentityRoleType identityRoleType,
                                  string name, string surname, string address, string phone)
            : base(userName, email, identityRoleType, name, surname, address, phone)
        { }

        /// <summary>
        /// Обновить личную информацию
        /// </summary>
        public IBoutiqueUserDomain UpdatePersonal(IPersonalDomain personal) =>
            UpdatePersonal(personal.Name, personal.Surname, personal.Address, personal.Phone);

        /// <summary>
        /// Обновить личную информацию
        /// </summary>
        public IBoutiqueUserDomain UpdatePersonal(string name, string surname, string address, string phone) =>
            new BoutiqueUserDomain(UserName, Email, IdentityRoleType, name, surname, address, phone);
    }
}