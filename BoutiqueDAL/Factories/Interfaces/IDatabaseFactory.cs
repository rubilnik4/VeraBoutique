using Functional.Models.Interfaces.Result;
using NHibernate;

namespace BoutiqueDAL.Factories.Interfaces
{
    /// <summary>
    /// Фабрика для создания сессии подключения к БД
    /// </summary>
    public interface IDatabaseFactory
    {
        /// <summary>
        /// Получить фабрику для создания сессии
        /// </summary>
        public IResultValue<ISessionFactory> SessionFactory { get; }
    }
}