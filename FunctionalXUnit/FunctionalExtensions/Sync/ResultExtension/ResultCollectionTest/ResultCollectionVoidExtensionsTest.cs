using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOk_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOk(numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOk_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = new ResultCollection<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOk(numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidBad_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidBad(
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidBad(
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOkWhere(number => true,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = new ResultCollection<int>(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOkWhere(number => false,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOkWhere(number => true,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultCollection<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOkWhere(number => false,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}