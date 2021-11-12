using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identities
{
    /// <summary>
    /// Пользователь. Трансферная модель
    /// </summary>
    public class BoutiqueUserTransfer: BoutiqueUserBase, IBoutiqueUserTransfer
    {
        public BoutiqueUserTransfer(IBoutiqueUserBase boutiqueUserBase)
            :base(boutiqueUserBase.UserName, boutiqueUserBase.Email, boutiqueUserBase.IdentityRoleType,
                  boutiqueUserBase.Name, boutiqueUserBase.Surname, boutiqueUserBase.Address, boutiqueUserBase.Phone)
        { }

        [JsonConstructor]
        public BoutiqueUserTransfer(string userName, string email, IdentityRoleType identityRoleType,
                                    string name, string surname, string address, string phone)
                :base(userName,  email,  identityRoleType, name,  surname,  address,  phone)
        { }
    }
}