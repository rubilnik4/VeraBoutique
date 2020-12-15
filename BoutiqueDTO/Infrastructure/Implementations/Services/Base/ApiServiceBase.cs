using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Base
{
    public abstract class ApiServiceBase<TId, TTransfer, TApi> : IApiServiceBase<TId, TTransfer>
        where TTransfer : ITransferModel<TId>
        where TId : notnull
        where TApi : IApiBase
    {
        /// <summary>
        /// Базовый интерфейс для Api методов
        /// </summary>
        protected abstract TApi ApiBase { get; }

        /// <summary>
        /// Получение данных
        /// </summary>
        protected abstract Task<IReadOnlyCollection<TTransfer>> GetApi();

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        protected abstract Task<TTransfer> GetApi(TId id);

        /// <summary>
        /// Добавление данных
        /// </summary>
        protected abstract Task<TId> PostApi(TTransfer transfer);

        /// <summary>
        /// Добавление коллекции данных
        /// </summary>
        protected abstract Task<IReadOnlyCollection<TId>> PostApi(IReadOnlyCollection<TTransfer> transfers);

        /// <summary>
        /// Добавление данных
        /// </summary>
        protected abstract Task PutApi(TTransfer transfer);

        /// <summary>
        /// Удаление данных
        /// </summary>
        protected abstract Task<TTransfer> DeleteApi(TId id);

        /// <summary>
        /// Получение данных
        /// </summary>
        public Task<IResultCollection<TTransfer>> Get() =>
            ResultCollectionTryAsync(GetApi, new ErrorResult(ErrorResultType.Unknown, String.Empty));

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        public Task<IResultValue<TTransfer>> Get(TId id) =>
            ResultValueTryAsync(() => GetApi(id), TransferError);

        /// <summary>
        /// Добавление данных
        /// </summary>
        public Task<IResultValue<TId>> Post(TTransfer transfer) =>
            ResultValueTryAsync(() => PostApi(transfer), TransferError);

        /// <summary>
        /// Добавление коллекции данных
        /// </summary>
        public Task<IResultCollection<TId>> Post(IReadOnlyCollection<TTransfer> transfers) =>
            ResultCollectionTryAsync(() => PostApi(transfers), TransferError);

        /// <summary>
        /// Добавление данных
        /// </summary>
        public Task<IResultError> Put(TTransfer transfer) =>
            ResultErrorTryAsync(() => PutApi(transfer), TransferError);

        /// <summary>
        /// Удаление данных
        /// </summary>
        public Task<IResultValue<TTransfer>> Delete(TId id) =>
            ResultValueTryAsync(() => DeleteApi(id), TransferError);

        private static IErrorResult TransferError => new ErrorResult(ErrorResultType.Unknown, String.Empty);
    }
}