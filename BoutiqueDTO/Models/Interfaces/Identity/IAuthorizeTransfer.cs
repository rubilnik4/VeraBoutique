using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Identity
{
    /// <summary>
    /// Имя пользователя и пароль. Трансферная модель
    /// </summary>
    public interface IAuthorizeTransfer: IAuthorizeBase, ITransferModel<(string,string)>
    { }
}