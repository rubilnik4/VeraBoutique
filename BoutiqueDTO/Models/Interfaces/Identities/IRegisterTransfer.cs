using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identities
{
    /// <summary>
    /// Регистрация. Трансферная модель
    /// </summary>
    public interface IRegisterTransfer : IRegisterBase<AuthorizeTransfer, PersonalTransfer>, ITransferModel<string>
    { }
}