using System.Threading.Tasks;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Models.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.FunctionalExtensions.ResultExtension
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
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkAsync_Bad_NotCallVoid()
        {
            var resultOk = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.
                                  ResultVoidOkAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadAsync_Ok_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultOk = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.
                                  ResultVoidBadAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultOk = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.
                                  ResultVoidBadAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
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
            var resultOk = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultVoidOkWhereAsync(number => number == 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var resultOk = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOk.
                ResultVoidOkWhereAsync(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }
    }
}