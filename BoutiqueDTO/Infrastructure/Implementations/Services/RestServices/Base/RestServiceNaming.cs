using System;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base
{
    /// <summary>
    /// Именование сервиса
    /// </summary>
    public abstract class RestServiceNaming<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Наименование контроллера
        /// </summary>
        protected string ControllerName =>
            GetType().Name.SubstringRemove(typeof(RestServiceBase<TId, TDomain, TTransfer>).GetNameWithoutGenerics());
    }
}