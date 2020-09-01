using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Enums;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionEnumerableAsync(1),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionEnumerableAsync(0),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionCollectionAsync(1),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionCollectionAsync(0),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_List()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionListAsync(1),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_List()
        {
            var resultValue = await ResultCollectionTryAsync(() => AsyncFunctions.DivisionListAsync(0),
                                                             Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, resultValue.Errors.First().ErrorResultType);
        }
    }
}