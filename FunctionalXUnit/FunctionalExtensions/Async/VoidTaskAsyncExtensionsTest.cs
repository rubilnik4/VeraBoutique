using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;

namespace FunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для действий задачи-объекта. Тесты
    /// </summary>
    public class VoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия
        /// </summary>
        [Fact]
        public async Task VoidTaskAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.
                                  VoidTaskAsync(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidOkTaskAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkTaskAsync(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public async Task VoidOkTaskAsync_CounterAddNone()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkTaskAsync(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }
    }
}