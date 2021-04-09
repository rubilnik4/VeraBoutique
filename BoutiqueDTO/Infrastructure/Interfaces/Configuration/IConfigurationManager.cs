using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации
    /// </summary>
    public interface IConfigurationManager<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        Task<string> GetConfigurationText();

        /// <summary>
        /// Получить конфигурацию асинхронно
        /// </summary>
        Task<IResultValue<TDomain>> GetConfiguration();
    }
}