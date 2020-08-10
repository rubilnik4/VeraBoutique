﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueDAL.Factories.Interfaces;
using NHibernate;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Класс обертка для управления транзакциями
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Сессия для подключения к базе
        /// </summary>
        public ISession Session { get; }

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            Session = sessionFactory.OpenSession();
            BeginTransaction();
        }

        /// <summary>
        /// Открываемая транзакция
        /// </summary>
        private ITransaction? _transaction;

        /// <summary>
        /// Открыть транзакцию
        /// </summary>
        private void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) =>
            _transaction = Session.BeginTransaction(isolationLevel);

        /// <summary>
        /// Подтвердить транзакцию
        /// </summary>
        public void Commit()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                _transaction.Commit();
            }
        }

        /// <summary>
        /// Подтвердить транзакцию асинхронно
        /// </summary>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null && _transaction.IsActive)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        /// <summary>
        /// Откатить транзакцию асинхронно
        /// </summary>
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null && _transaction.IsActive)
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }

        #region IDisposable Support
        private bool _disposedValue;

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {

            }

            _transaction?.Dispose();
            Session?.Dispose();

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