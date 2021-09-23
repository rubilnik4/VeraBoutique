using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommon.Models.Domain.Implementations.Identity
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