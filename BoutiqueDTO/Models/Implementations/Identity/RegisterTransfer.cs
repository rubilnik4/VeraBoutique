using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// Регистрация. Трансферная модель
    /// </summary>
    public class RegisterTransfer : RegisterBase, IRegisterTransfer
    {
        public RegisterTransfer(IRegisterBase register)
           : this(register.Email, register.Password, register.Name, register.Surname, register.Address, register.Phone)
        { }

        [JsonConstructor]
        public RegisterTransfer(string email, string password, string name, string surname, string address, string phone)
            : base(email, password, name, surname, address, phone)
        { }
    }
}