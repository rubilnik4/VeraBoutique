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
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        string GetConfigurationText();

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        Task<string> GetConfigurationTextAsync();

        /// <summary>
        /// Получить конфигурацию
        /// </summary>
        IResultValue<TDomain> GetConfiguration();

        /// <summary>
        /// Получить конфигурацию асинхронно
        /// </summary>
        Task<IResultValue<TDomain>> GetConfigurationAsync();
    }
}