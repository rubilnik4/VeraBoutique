using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

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
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        public async Task<IResultError> SaveChangesAsync() => await ResultErrorTryAsync(() => base.SaveChangesAsync(),
                                                                                        DatabaseErrors.DatabaseSaveError());
    }
}