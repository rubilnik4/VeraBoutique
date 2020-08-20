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

        /// <summary>
        /// Использовать класс-обертку, выполнить действие и закрыть
        /// </summary>
        Task<IResultError> UseActionAsync(Func<ISession, Task> action);

        /// <summary>
        /// Использовать класс-обертку и закрыть
        /// </summary>
        Task <IResultValue<TValue>> UseFuncAsync<TValue>(Func<ISession, Task<TValue>> func);

        /// <summary>
        /// Подтвердить транзакцию асинхронно и закрыть объект
        /// </summary>
        Task<IResultError> UseActionAndCommitAsync(Func<ISession, Task> action);

        /// <summary>
        /// Подтвердить транзакцию асинхронно и закрыть объект
        /// </summary>
        Task<IResultValue<TValue>> UseFuncAndCommitAsync<TValue>(Func<ISession, Task<TValue>> func);
    }
}