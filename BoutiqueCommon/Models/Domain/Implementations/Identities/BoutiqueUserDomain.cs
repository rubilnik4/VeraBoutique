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
                  boutiqueUser.Name, boutiqueUser.Surname, boutiqueUser.Address, boutiqueUser.PhoneNumber)
        { }

        public BoutiqueUserDomain(string userName, string email, IdentityRoleType identityRoleType,
                                  string name, string surname, string address, string phoneNumber)
            : base(userName, email, identityRoleType, name, surname, address, phoneNumber)
        { }
    }
}