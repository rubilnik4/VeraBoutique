using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации
    /// </summary>
    public interface IConfigurationManager<TId, out TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        string GetConfigurationText();

        /// <summary>
        /// Получить конфигурацию асинхронно
        /// </summary>
        IResultValue<TDomain> GetConfiguration();
    }
}