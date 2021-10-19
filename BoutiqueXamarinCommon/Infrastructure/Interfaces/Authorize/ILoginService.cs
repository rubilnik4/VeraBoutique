using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize
{
    /// <summary>
    /// Сервис авторизации и сохранения логина
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Авторизоваться через токен JWT
        /// </summary>
        Task<IResultError> Login(IAuthorizeDomain authorize);
    }
}