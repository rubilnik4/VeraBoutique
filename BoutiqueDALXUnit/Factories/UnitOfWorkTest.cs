using System;
using System.Data;
using System.Threading.Tasks;
using BoutiqueDAL.Factories.Implementations;
using Functional.Models.Implementations.Result;
using Moq;
using NHibernate;
using Xunit;

namespace BoutiqueDALXUnit.Factories
{
    /// <summary>
    /// Класс обертка для управления транзакциями. Тесты
    /// </summary>
    public class UnitOfWorkTest
    {
        /// <summary>
        /// Использовать класс-обертку, выполнить действие и закрыть
        /// </summary>
        [Fact]
        public async Task UseActionAsync_InvokeAndDispose()
        {
            var (sessionMock, transactionMock) = GetSessionMock();

            var sessionResult = new ResultValue<ISession>(sessionMock.Object);
            var unitOfWork = new UnitOfWork(sessionResult);

            await unitOfWork.UseActionAsync(session => session.FlushAsync());

            transactionMock.Verify(transaction => transaction.Dispose(), Times.Once);
            sessionMock.Verify(session => session.FlushAsync(default), Times.Once);
            sessionMock.Verify(session => session.Dispose(), Times.Once);
        }

        /// <summary>
        /// Использовать класс-обертку, выполнить функцию и закрыть
        /// </summary>
        [Fact]
        public async Task UseFuncAsync_InvokeAndDispose()
        {
            var (sessionMock, transactionMock) = GetSessionMock();

            var sessionResult = new ResultValue<ISession>(sessionMock.Object);
            var unitOfWork = new UnitOfWork(sessionResult);

            await unitOfWork.UseFuncAsync(session => session.MergeAsync(String.Empty));

            transactionMock.Verify(transaction => transaction.Dispose(), Times.Once);
            sessionMock.Verify(session => session.MergeAsync(It.IsAny<string>(), default), Times.Once);
            sessionMock.Verify(session => session.Dispose(), Times.Once);
        }

        /// <summary>
        /// Подтвердить транзакцию асинхронно и закрыть объект
        /// </summary>
        [Fact]
        public async Task UseActionAndCommitAsync_InvokeAndDispose()
        {
            var (sessionMock, transactionMock) = GetSessionMock();

            var sessionResult = new ResultValue<ISession>(sessionMock.Object);
            var unitOfWork = new UnitOfWork(sessionResult);

            await unitOfWork.UseActionAndCommitAsync(session => session.FlushAsync());

            transactionMock.Verify(transaction => transaction.CommitAsync(default), Times.Once);
            transactionMock.Verify(transaction => transaction.Dispose(), Times.Once);
            sessionMock.Verify(session => session.FlushAsync(default), Times.Once);
            sessionMock.Verify(session => session.Dispose(), Times.Once);
        }

        /// <summary>
        /// Использовать класс-обертку, выполнить функцию и закрыть
        /// </summary>
        [Fact]
        public async Task UseFuncAndCommitAsync_InvokeAndDispose()
        {
            var (sessionMock, transactionMock) = GetSessionMock();

            var sessionResult = new ResultValue<ISession>(sessionMock.Object);
            var unitOfWork = new UnitOfWork(sessionResult);

            await unitOfWork.UseFuncAndCommitAsync(session => session.MergeAsync(String.Empty));

            transactionMock.Verify(transaction => transaction.CommitAsync(default), Times.Once);
            transactionMock.Verify(transaction => transaction.Dispose(), Times.Once);
            sessionMock.Verify(session => session.MergeAsync(It.IsAny<string>(), default), Times.Once);
            sessionMock.Verify(session => session.Dispose(), Times.Once);
        }

        /// <summary>
        /// Получить тестовый экземпляр сессии
        /// </summary>
        private static (Mock<ISession>, Mock<ITransaction>) GetSessionMock()
        {
            var sessionMock = new Mock<ISession>();
            var transactionMock = new Mock<ITransaction>();

            sessionMock.Setup(session => session.BeginTransaction(It.IsAny<IsolationLevel>())).
                        Returns(transactionMock.Object);
            sessionMock.Setup(session => session.FlushAsync(default)).Returns(Task.CompletedTask);
            sessionMock.Setup(session => session.MergeAsync(It.IsAny<string>(), default)).
                        ReturnsAsync(String.Empty);

            transactionMock.Setup(transaction => transaction.IsActive).Returns(true);
            transactionMock.Setup(transaction => transaction.CommitAsync(default)).
                            Returns(Task.CompletedTask);

            return (sessionMock, transactionMock);
        }
    }
}