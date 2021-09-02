using System;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base
{
    /// <summary>
    /// Именование сервиса
    /// </summary>
    public abstract class RestService<TId, TDomain, TTransfer>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName =>
            typeof(RestService<TId, TDomain, TTransfer>).GetNameWithoutGenerics().
            Map(baseName => GetType().Name.SubstringRemove(baseName));
    }
}