using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;

namespace BoutiqueCommon.Models.Domain.Implementations.Identities
{
    /// <summary>
    /// Личная информация. Доменная модель
    /// </summary>
    public class PersonalDomain : PersonalBase, IPersonalDomain
    {
        public PersonalDomain(IPersonalBase personal)
            : this(personal.Name, personal.Surname, personal.Address, personal.Phone)
        { }

        public PersonalDomain(string name, string surname, string address, string phone)
            : base(name, surname, address, phone)
        { }
    }
}