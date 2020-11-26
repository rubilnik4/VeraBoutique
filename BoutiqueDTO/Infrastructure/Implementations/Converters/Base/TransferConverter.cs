using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

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
        /// Преобразовать тип пола одежды в доменную модель
        /// </summary>
        public IResultValue<TDomain> GetDomain(TTransfer? transfer) =>
            transfer.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(typeof(TTransfer).Name)).
            ResultValueBindOk(FromTransfer);

        /// <summary>
        /// Преобразовать типы пола одежды в доменную модель
        /// </summary>
        public IResultCollection<TDomain> GetDomains(IEnumerable<TTransfer>? transfers) =>
            transfers.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(typeof(IEnumerable<TTransfer>).Name)).
            ResultValueBindOkToCollection(FromTransfers);
    }
}