using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base
{
    /// <summary>
    /// Базовый сервис для получения данных Api
    /// </summary>
    public abstract class RestServiceBase<TId, TDomain, TTransfer> : IRestServiceBase<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected RestServiceBase(IApiService<TId, TTransfer> apiService, 
                                  ITransferConverter<TId, TDomain, TTransfer> transferConverter, 
                                  ILogger logger)
        {
            _apiService = apiService;
            _transferConverter = transferConverter;
            _logger = logger;
        }

        /// <summary>
        /// Сервис получения данных по протоколу rest api
        /// </summary>
        private readonly IApiService<TId, TTransfer> _apiService;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _transferConverter;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Загрузить типы пола
        /// </summary>
        public async Task<IResultError> Upload(IEnumerable<TDomain> domains) =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueVoidOk(_ => _logger.ShowMessage($"Загрузка [{typeof(TDomain).Name}]")).
            ResultValueBindOkToCollectionAsync(api => api.PostCollection(_transferConverter.ToTransfers(domains))).
            ResultCollectionVoidOkBadTaskAsync(ids => ids.Void(_ => _logger.ShowMessage($"Загрузка [{typeof(TDomain).Name}] завершена успешно")),
                                               errors => errors.
                                                         Void(_ => _logger.ShowMessage($"Ошибка загрузки [{typeof(TDomain).Name}]")).
                                                         Void(_ => _logger.ShowErrors(errors)));
    }
}