using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identities
{
    /// <summary>
    /// Имя пользователя и пароль. Трансферная модель
    /// </summary>
    public interface IAuthorizeTransfer: IAuthorizeBase, ITransferModel<string>
    { }
}