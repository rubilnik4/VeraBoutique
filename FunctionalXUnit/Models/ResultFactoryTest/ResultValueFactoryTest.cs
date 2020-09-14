using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Implementations.ResultFactory;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.Models.ResultFactoryTest
{

    /// <summary>
    /// Фабрика для создания результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await ResultValueFactory.CreateTaskResultValue(initialValue);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(initialValue, resultValue.Value);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await ResultValueFactory.CreateTaskResultValueAsync(initialValue);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(initialValue, resultValue.Value);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultValueFactory.CreateTaskResultValueErrorAsync<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultValueFactory.CreateTaskResultValueErrorAsync<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }
    }
}