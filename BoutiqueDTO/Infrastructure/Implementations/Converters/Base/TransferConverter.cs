using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Errors;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в трансферную модель
    /// </summary>
    public abstract class TransferConverter<TId, TDomain, TTransfer> : ITransferConverter<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Преобразовать модель базы данных в доменную
        /// </summary>
        public abstract IResultValue<TDomain> FromTransfer(TTransfer entity);

        /// <summary>
        /// Преобразовать доменную модель в модель базы данных
        /// </summary>
        public abstract TTransfer ToTransfer(TDomain domain);

        /// <summary>
        /// Преобразовать модели базы данных в доменные
        /// </summary>
        public IResultCollection<TDomain> FromTransfers(IEnumerable<TTransfer> entities) =>
            entities.Select(FromTransfer).ToResultCollection();

        /// <summary>
        /// Преобразовать доменные модели в модели базы данных
        /// </summary>
        public IEnumerable<TTransfer> ToTransfers(IEnumerable<TDomain> domains) =>
            domains.Select(ToTransfer);

        /// <summary>
        /// Преобразовать в доменную модель
        /// </summary>
        public IResultValue<TDomain> GetDomain(TTransfer? transfer) =>
            transfer.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(transfer, GetType())).
            ResultValueBindOk(FromTransfer);

        /// <summary>
        /// Преобразовать в доменные модели
        /// </summary>
        public IResultCollection<TDomain> GetDomains(IReadOnlyCollection<TTransfer>? transfers) =>
            transfers.
            ToResultCollectionNullCheck(ErrorResultFactory.ValueNotFoundError(transfers, GetType())).
            ResultCollectionBindOk(FromTransfers);
    }
}