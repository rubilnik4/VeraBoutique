using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Implementations.ResultFactory;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.Models.ResultsFactory
{

    /// <summary>
    /// Фабрика для создания результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollection(initialValue);

            Assert.True(resultValue.OkStatus);
            Assert.True(initialValue.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollectionAsync(initialValue);

            Assert.True(resultValue.OkStatus);
            Assert.True(initialValue.SequenceEqual(resultValue.Value));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollectionErrorAsync<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultCollectionFactory.CreateTaskResultCollectionErrorAsync<int>(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }
    }
}