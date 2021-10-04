using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identity
{
    /// <summary>
    /// Регистрация. Трансферная модель
    /// </summary>
    public interface IRegisterTransfer : IRegisterBase<AuthorizeTransfer, PersonalTransfer>, ITransferModel<string>
    { }
}