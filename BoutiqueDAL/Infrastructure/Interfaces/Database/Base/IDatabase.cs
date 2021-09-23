﻿using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base
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
        /// Отключить отслеживание сущностей
        /// </summary>
        void Detach();

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        Task UpdateSchema(UserManager<BoutiqueUser> userManager, IResultCollection<BoutiqueRoleUser> defaultUsers);
    }
}