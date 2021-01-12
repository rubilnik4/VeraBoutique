using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization
{
    /// <summary>
    /// Api сервис авторизации
    /// </summary>
    public interface IAuthorizeApiService: IApiServiceBase
    {
        /// <summary>
        /// Авторизоваться через JWT токен
        /// </summary>
        Task<IResultValue<string>> AuthorizeJwt(AuthorizeTransfer authorizeTransfer);
    }
}