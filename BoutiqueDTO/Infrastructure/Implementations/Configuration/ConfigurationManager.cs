using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

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
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        public abstract string GetConfigurationText();

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public abstract Task<string> GetConfigurationTextAsync();

        /// <summary>
        /// Получить конфигурацию в текстовом виде
        /// </summary>
        public IResultValue<TDomain> GetConfiguration() =>
            GetConfigurationText().
            ToTransferValueJson<TTransfer>().
            ResultValueBindOk(configTransfer => _configurationConverter.FromTransfer(configTransfer));

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> GetConfigurationAsync() =>
            await GetConfigurationTextAsync().
            ToTransferValueJsonAsync<TTransfer>().
            ResultValueBindOkTaskAsync(configTransfer => _configurationConverter.FromTransfer(configTransfer));
    }
}