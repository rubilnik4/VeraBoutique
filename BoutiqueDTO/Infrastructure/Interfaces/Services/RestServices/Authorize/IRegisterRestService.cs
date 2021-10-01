using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    public interface IRegisterRestService
    {
        /// <summary>
        /// Зарегистрироваться в сервисе
        /// </summary>
        Task<IResultValue<string>> Register(IRegisterDomain registerDomain);
    }
}