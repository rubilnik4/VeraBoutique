using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identity
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