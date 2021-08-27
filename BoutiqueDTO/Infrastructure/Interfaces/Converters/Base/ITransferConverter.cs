using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в трансферную модель
    /// </summary>
    public interface ITransferConverter<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Преобразовать трансферную модель в доменную
        /// </summary>
        IResultValue<TDomain> FromTransfer(TTransfer transfer);

        /// <summary>
        /// Преобразовать доменную модель в трансферную модель
        /// </summary>
        TTransfer ToTransfer(TDomain domain);

        /// <summary>
        /// Преобразовать трансферные модели в доменные
        /// </summary>
        IResultCollection<TDomain> FromTransfers(IEnumerable<TTransfer> transfers);

        /// <summary>
        /// Преобразовать доменные модели в трансферную модели
        /// </summary>
        IEnumerable<TTransfer> ToTransfers(IEnumerable<TDomain> domains);

        /// <summary>
        /// Преобразовать тип пола одежды в доменную модель
        /// </summary>
        IResultValue<TDomain> GetDomain(TTransfer? transfer);

        /// <summary>
        /// Преобразовать типы пола одежды в доменную модель
        /// </summary>
        IResultCollection<TDomain> GetDomains(IReadOnlyCollection<TTransfer>? transfers);
    }
}