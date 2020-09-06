using System.Linq;
using System.Threading.Tasks;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using FunctionalXUnit.Mocks.Implementation;
using Xunit;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static FunctionalXUnit.Data.Collections;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.AsyncFunctions;

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

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, CreateErrorTest());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, CreateErrorTest());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult =new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numberResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numbersAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}