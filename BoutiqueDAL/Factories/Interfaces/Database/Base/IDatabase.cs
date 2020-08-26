using System.Threading.Tasks;

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
        Task SaveChangesAsync();

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        void UpdateSchema();
    }
}