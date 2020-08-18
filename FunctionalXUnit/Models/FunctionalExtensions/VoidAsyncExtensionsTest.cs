using System.Threading.Tasks;
using Functional.FunctionalExtensions;
using FunctionalXUnit.Models.Mocks.Interfaces;
using Moq;
using Xunit;

namespace FunctionalXUnit.Models.FunctionalExtensions
{
    /// <summary>
    /// Методы расширения для асинхронных действий. Тесты
    /// </summary>
    public class VoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия
        /// </summary>
        [Fact]
        public async Task VoidAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await initialNumber.
                                  VoidAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidOkAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await initialNumber.
                VoidOkAsync(number => number > 0,
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
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await initialNumber.
                VoidOkAsync(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }
    }
}