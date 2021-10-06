using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identities
{
    /// <summary>
    /// Личная информация. Трансферная модель 
    /// </summary>
    public class PersonalTransfer : PersonalBase, IPersonalTransfer
    {
        public PersonalTransfer(IPersonalBase personal)
          : this(personal.Name, personal.Surname, personal.Address, personal.Phone)
        { }

        [JsonConstructor]
        public PersonalTransfer(string name, string surname, string address, string phone)
            : base(name, surname, address, phone)
        { }
    }
}