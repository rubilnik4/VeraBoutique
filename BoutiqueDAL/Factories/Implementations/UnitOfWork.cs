using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueDAL.Factories.Interfaces;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Interfaces.Result;
using NHibernate;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Класс обертка для управления транзакциями
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IResultValue<ISession> session)
        {
            Session = session;
            _transaction = BeginTransaction(session);
        }

        /// <summary>
        /// Сессия для подключения к базе
        /// </summary>
        public IResultValue<ISession> Session { get; }

        /// <summary>
        /// Открываемая транзакция
        /// </summary>
        private readonly IResultValue<ITransaction> _transaction;

        /// <summary>
        /// Открыть транзакцию
        /// </summary>
        private static IResultValue<ITransaction> BeginTransaction(IResultValue<ISession> sessionResult) =>
            sessionResult.ResultValueOk(session => session.BeginTransaction(IsolationLevel.ReadCommitted));

        /// <summary>
        /// Подтвердить транзакцию
        /// </summary>
        public void Commit() =>
            _transaction?.
            ResultVoidOkWhere(transaction => transaction?.IsActive == true,
                action: transaction => transaction.Commit());

        /// <summary>
        /// Подтвердить транзакцию асинхронно
        /// </summary>
        public async Task CommitAsync(CancellationToken cancellationToken = default) =>
            await _transaction.
            ResultVoidOkWhereAsync(transaction => transaction?.IsActive == true,
                action: transaction => transaction.CommitAsync(cancellationToken));

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        public void Rollback() =>
            _transaction?.
            ResultVoidOkWhere(transaction => transaction?.IsActive == true,
                action: transaction => transaction.Rollback());

        /// <summary>
        /// Откатить транзакцию асинхронно
        /// </summary>
        public async Task RollbackAsync(CancellationToken cancellationToken = default) =>
            await _transaction.
            ResultVoidOkWhereAsync(transaction => transaction?.IsActive == true,
                action: transaction => transaction.RollbackAsync(cancellationToken));

        /// <summary>
        /// Использовать класс-обертку и закрыть
        /// </summary>
        public async Task<IResultValue<TValue>> Use<TValue>(Func<ISession, Task<TValue>> func) =>
            await Session.ResultValueOkAsync(func.Invoke).
            Void(_ => Dispose());

        /// <summary>
        /// Подтвердить транзакцию асинхронно и закрыть объект
        /// </summary>
        public async Task<IResultValue<TValue>> UseAndCommitAsync<TValue>(Func<ISession, Task<TValue>> func) =>
            await Session.ResultValueOkAsync(func.Invoke).
            VoidBindAsync(_ => CommitAsync()).
            Void(_ => Dispose());

        #region IDisposable Support
        private bool _disposedValue;

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {

            }

            _transaction?.ResultVoidOk(transaction => transaction.Dispose());
            Session?.ResultVoidOk(session => session.Dispose());

            _disposedValue = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}