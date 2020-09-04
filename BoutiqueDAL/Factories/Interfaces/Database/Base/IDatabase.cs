﻿using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Interfaces.Database.Base
{
    /// <summary>
    /// Шаблон базы данных
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        Task<IResultError> SaveChangesAsync();

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        void UpdateSchema();
    }
}