using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис личной информации
    /// </summary>
    public interface IProfileRestService
    {
        /// <summary>
        /// Получить личные данные пользователя
        /// </summary>
        Task<IResultValue<IBoutiqueUserDomain>> GetProfile();
    }
}