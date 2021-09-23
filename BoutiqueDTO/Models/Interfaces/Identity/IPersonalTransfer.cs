using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identity
{
    /// <summary>
    /// Личная информация. Трансферная модель
    /// </summary>
    public interface IPersonalTransfer : IPersonalBase, ITransferModel<string>
    { }
}