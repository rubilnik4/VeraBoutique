using System;
using Functional.FunctionalExtensions;
using FunctionalXUnit.Models.Mocks.Interfaces;
using Moq;
using Xunit;

namespace FunctionalXUnit.Models.FunctionalExtensions
{
    /// <summary>
    /// Методы расширения для действий. Тесты
    /// </summary>
    public class VoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия
        /// </summary>
        [Fact]
        public void Void_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = initialNumber.Void(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при положительном условии
        /// </summary>
        [Fact]
        public void VoidOk_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = 
                initialNumber.
                VoidOk(number => number > 0, 
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public void VoidOk_CounterAddNone()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                initialNumber.
                VoidOk(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }
    }
}