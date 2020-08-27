using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkAsync_Ok_CallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.
                                  ResultVoidOkAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = new ResultValue<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.
                                  ResultVoidOkAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadAsync_Ok_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.
                                  ResultVoidBadAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.
                                  ResultVoidBadAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(errorsInitial.Count), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultVoidOkWhereAsync(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            const int initialNumber = 1;
            var resultOk = new ResultValue<int>(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultVoidOkWhereAsync(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var resultError = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultError.
                ResultVoidOkWhereAsync(number => number == 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var resultError = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultError.
                ResultVoidOkWhereAsync(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }
    }
}