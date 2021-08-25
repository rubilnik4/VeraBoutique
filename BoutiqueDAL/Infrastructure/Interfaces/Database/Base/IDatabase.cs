using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.Models.Interfaces.Results;
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
        Task UpdateSchema(UserManager<IdentityUser> userManager, IResultCollection<BoutiqueUser> defaultUsers);
    }
}