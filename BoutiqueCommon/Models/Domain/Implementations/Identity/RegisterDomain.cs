using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommon.Models.Domain.Implementations.Identity
{
    /// <summary>
    /// Регистрация. Доменная модель
    /// </summary>
    public class RegisterDomain : RegisterBase, IRegisterDomain
    {
        public RegisterDomain(IRegisterBase register)
            : this(register.Email, register.Password, register.Name, register.Surname, register.Address, register.Phone)
        { }

        public RegisterDomain(string email, string password, string name, string surname, string address, string phone)
            : base(email, password, name, surname, address, phone)
        { }
    }
}