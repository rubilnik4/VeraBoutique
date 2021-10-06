using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identities
{
    /// <summary>
    /// Личная информация. Трансферная модель
    /// </summary>
    public interface IPersonalTransfer : IPersonalBase, ITransferModel<string>
    { }
}