﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Функции выгрузки данных
    /// </summary>
    public interface IDatabaseTableToList<TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Вернуть полные записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListMainAsync();
    }
}