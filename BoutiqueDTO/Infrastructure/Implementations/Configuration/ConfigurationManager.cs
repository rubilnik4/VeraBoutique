using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации
    /// </summary>
    public abstract class ConfigurationManager<TId, TDomain, TTransfer> : IConfigurationManager<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected ConfigurationManager(ITransferConverter<TId, TDomain, TTransfer> configurationConverter)
        {
            _configurationConverter = configurationConverter;
        }

        ///<summary>
        /// Конвертер конфигурации в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _configurationConverter;

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public abstract Task<string> GetConfigurationText();
        
        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> GetConfiguration() =>
            await GetConfigurationText().
            ToTransferValueJsonAsync<TTransfer>().
            ResultValueBindOkTaskAsync(configTransfer => _configurationConverter.FromTransfer(configTransfer));
    }
}