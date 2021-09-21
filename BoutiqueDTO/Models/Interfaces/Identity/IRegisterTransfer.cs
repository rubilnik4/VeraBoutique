using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identity
{
    /// <summary>
    /// Регистрация. Трансферная модель
    /// </summary>
    public interface IRegisterTransfer : IRegisterBase, ITransferModel<string>
    { }
}