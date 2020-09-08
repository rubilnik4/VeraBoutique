﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Base
{
    /// <summary>
    /// Сервис получения данных из базы
    /// </summary>
    public interface IDatabaseService<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить модели из базы
        /// </summary>
        Task<IResultCollection<TDomain>> Get();

        /// <summary>
        /// Получить модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TDomain>> Get(TId id);

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        Task<IResultCollection<TId>> Post(IEnumerable<TDomain> models);

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        Task<IResultError> Put(TId id, TDomain model);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TDomain>> Delete(TId id);
    }
}