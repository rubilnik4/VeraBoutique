using System;
using System.Text;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Extensions.Json;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueXamarin.Infrastructure.Interfaces;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Models.Implementation.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Newtonsoft.Json;
using Prism;

namespace BoutiqueXamarin.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам
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
            ToTransferJson<TId, TTransfer>().
            ResultValueBindOk(configTransfer => _configurationConverter.FromTransfer(configTransfer));

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> GetConfigurationAsync() =>
            await GetConfigurationTextAsync().
            ToTransferJsonAsync<TId, TTransfer>().
            ResultValueBindOkTaskAsync(configTransfer => _configurationConverter.FromTransfer(configTransfer));
    }
}