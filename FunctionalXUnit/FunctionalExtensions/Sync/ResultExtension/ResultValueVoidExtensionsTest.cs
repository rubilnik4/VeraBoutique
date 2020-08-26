using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultVoidOk_Ok_CallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultVoidOk(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void ResultVoidOk_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = new ResultValue<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultVoidOk(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void ResultVoidBad_Ok_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void ResultVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(errorsInitial.Count), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultVoidOkWhere_Ok_OkPredicate_CallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = 
                resultOk.
                ResultVoidOkWhere(number => number > 0, 
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultVoidOkWhere_Ok_BadPredicate_NotCallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                resultOk.
                ResultVoidOkWhere(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultVoidOkWhere_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                resultError.
                ResultVoidOkWhere(number => number == 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultVoidOkWhere_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = 
                resultError.
                ResultVoidOkWhere(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }
    }
}