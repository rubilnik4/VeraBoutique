using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthorizeRestService
    {
        /// <summary>
        /// Авторизироваться в сервисе
        /// </summary>
        Task<IResultValue<string>> AuthorizeJwt(IAuthorizeDomain authorizeDomain);
    }
}