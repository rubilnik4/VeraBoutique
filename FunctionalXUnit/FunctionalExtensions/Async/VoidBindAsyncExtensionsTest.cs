using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;

namespace FunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронных действий. Тесты
    /// </summary>
    public class VoidBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия
        /// </summary>
        [Fact]
        public async Task VoidBindAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.
                                  VoidBindAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidOkBindAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkBindAsync(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public async Task VoidOkAsync_CounterAddNone()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkBindAsync(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }
    }
}