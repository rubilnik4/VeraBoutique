using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в трансферную модель
    /// </summary>
    public interface ITransferConverter<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : ITransferModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Преобразовать трансферную модель в доменную
        /// </summary>
        TDomain FromTransfer(TTransfer transfer);

        /// <summary>
        /// Преобразовать доменную модель в трансферную модель
        /// </summary>
        TTransfer ToTransfer(TDomain domain);

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        IResultValue<TDomain> FromJson(string json);

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        IResultValue<string> ToJson(TDomain domain);

        /// <summary>
        /// Преобразовать Json в коллекцию
        /// </summary>
        IResultCollection<TDomain> FromJsonCollection(string json);

        /// <summary>
        /// Преобразовать коллекцию типа пола в Json
        /// </summary>
        IResultValue<string> ToJsonCollection(IEnumerable<TDomain> domains);

        /// <summary>
        /// Преобразовать трансферные модели в доменные
        /// </summary>
        public IEnumerable<TDomain> FromTransfers(IEnumerable<TTransfer> transfes) =>
            transfes.Select(FromTransfer);

        /// <summary>
        /// Преобразовать доменные модели в трансферную модели
        /// </summary>
        public IEnumerable<TTransfer> ToTransfers(IEnumerable<TDomain> domains) =>
            domains.Select(ToTransfer);
    }
}