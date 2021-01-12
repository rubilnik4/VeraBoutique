using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Authorization
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