using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using Functional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base
{
    /// <summary>
    /// Базовая база данных Entity Framework
    /// </summary>
    public abstract class EntityDatabase : IdentityDbContext<IdentityUser>
    {
        protected EntityDatabase(DbContextOptions options)
          : base(options)
        { }

        /// <summary>
        /// Отключить отслеживание сущностей
        /// </summary>
        public void Detach()
        {
            var changedEntriesCopy = TrackedEntities;
            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }
        /// <summary>
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        public async Task<IResultError> SaveChangesAsync() =>
            await ResultErrorTryAsync(() => base.SaveChangesAsync(), DatabaseErrors.DatabaseSaveError());

        /// <summary>
        /// Отслеживаемые сущности
        /// </summary>
        private IEnumerable<EntityEntry> TrackedEntities =>
            ChangeTracker.Entries().
            Where(e => e.State == EntityState.Added ||
                       e.State == EntityState.Modified ||
                       e.State == EntityState.Deleted).
            ToList();
    }
}