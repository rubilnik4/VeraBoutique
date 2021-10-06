using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identities
{
    /// <summary>
    /// Пользователь. Трансферная модель
    /// </summary>
    public interface IBoutiqueUserTransfer : IBoutiqueUserBase, ITransferModel<string>
    { }
}