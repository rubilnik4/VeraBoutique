using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base
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
                                  IBoutiqueLogger boutiqueLogger)
        {
            _apiService = apiService;
            _transferConverter = transferConverter;
            _boutiqueLogger = boutiqueLogger;
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
        private readonly IBoutiqueLogger _boutiqueLogger;

        /// <summary>
        /// Загрузить данные
        /// </summary>
        public async Task<IResultError> Upload(IEnumerable<TDomain> domains) =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueVoidOk(_ => _boutiqueLogger.ShowMessage($"Загрузка [{typeof(TDomain).Name}]")).
            ResultValueBindOkToCollectionAsync(api => api.PostCollection(_transferConverter.ToTransfers(domains))).
            ResultCollectionVoidOkBadTaskAsync(ids => ids.Void(_ => _boutiqueLogger.ShowMessage($"Загрузка [{typeof(TDomain).Name}] завершена успешно")),
                                               errors => errors.
                                                         Void(_ => _boutiqueLogger.ShowMessage($"Ошибка загрузки [{typeof(TDomain).Name}]")).
                                                         Void(_ => _boutiqueLogger.ShowErrors(errors)));

        /// <summary>
        /// Удалить все данные
        /// </summary>
        public async Task<IResultError> Delete() =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueVoidOk(_ => _boutiqueLogger.ShowMessage($"Удаление [{typeof(TDomain).Name}]")).
            ResultValueBindErrorsOkAsync(api => api.Delete()).
            ResultValueVoidOkBadTaskAsync(ids => ids.Void(_ => _boutiqueLogger.ShowMessage($"Удаление [{typeof(TDomain).Name}] завершено успешно")),
                                               errors => errors.
                                                         Void(_ => _boutiqueLogger.ShowMessage($"Ошибка удаления [{typeof(TDomain).Name}]")).
                                                         Void(_ => _boutiqueLogger.ShowErrors(errors)));
    }
}