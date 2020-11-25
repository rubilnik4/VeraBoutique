using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
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
        where TTransfer : ITransferModel<TId>
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
    }
}