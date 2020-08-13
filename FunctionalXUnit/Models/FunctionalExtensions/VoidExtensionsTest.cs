using System;
using Functional.FunctionalExtensions;
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
            const string test = "test";
            int counter = 0;
            void ActionCounter() => counter += 1;

            string testAfterVoid = test.Void(_ => ActionCounter());

            Assert.True(testAfterVoid.Equals(test));
            Assert.Equal(1, counter);
        }
    }
}