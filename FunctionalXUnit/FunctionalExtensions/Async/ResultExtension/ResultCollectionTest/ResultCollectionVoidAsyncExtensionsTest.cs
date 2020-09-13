using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkAsync_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.
                                  ResultCollectionVoidOkAsync(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = new ResultCollection<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.
                                  ResultCollectionVoidOkAsync(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultCollectionVoidBadAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultCollectionVoidBadAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultCollectionVoidOkWhereAsync(numbers => true,
                    action: numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultCollectionVoidOkWhereAsync(numbers => false,
                    action: numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var resultError = new ResultCollection<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultError.
                ResultCollectionVoidOkWhereAsync(numbers => true,
                    action: numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var resultError = new ResultCollection<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultError.
                ResultCollectionVoidOkWhereAsync(numbers => false,
                    action: numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}