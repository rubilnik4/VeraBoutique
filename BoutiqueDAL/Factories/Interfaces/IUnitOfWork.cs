using System;
using System.Threading;
using System.Threading.Tasks;
using Functional.Models.Interfaces.Result;
using NHibernate;

namespace BoutiqueDAL.Factories.Interfaces
{
    /// <summary>
    /// Класс обертка для управления транзакциями
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сессия для подключения к базе
        /// </summary>
        IResultValue<ISession> Session { get; }

        /// <summary>
        /// Подтвердить транзакцию
        /// </summary>
        void Commit();

        /// <summary>
        /// Подтвердить транзакцию асинхронно
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        void Rollback();

        /// <summary>
        /// Откатить транзакцию асинхронно
        /// </summary>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}