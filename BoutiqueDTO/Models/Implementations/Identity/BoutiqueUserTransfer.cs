using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// Пользователь. Трансферная модель
    /// </summary>
    public class BoutiqueUserTransfer: BoutiqueUser
    {
        public BoutiqueUserTransfer(IBoutiqueUser boutiqueUser)
            :base(boutiqueUser.UserName, boutiqueUser.Email, boutiqueUser.IdentityRoleType,
                  boutiqueUser.Name, boutiqueUser.Surname, boutiqueUser.Address, boutiqueUser.PhoneNumber)
        { }

        [JsonConstructor]
        public BoutiqueUserTransfer(string userName, string email, IdentityRoleType identityRoleType,
                                    string name, string surname, string address, string phoneNumber)
                :base(userName,  email,  identityRoleType, name,  surname,  address,  phoneNumber)
        { }
    }
}