using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением задачей-объектом.Тесты
    /// </summary>
    public class ResultValueVoidBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkBindAsync_Ok_CallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.
                                  ResultVoidOkBindAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultVoidOkBindAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadBindAsync_Ok_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultVoidBadBindAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultVoidBadBindAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(errorsInitial.Count), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOkTask.
                ResultVoidOkWhereBindAsync(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOkTask.
                ResultVoidOkWhereBindAsync(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultErrorTask.
                ResultVoidOkWhereBindAsync(number => number == 0,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultErrorTask.
                ResultVoidOkWhereBindAsync(number => number > 2,
                    action: number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(0), Times.Never);
        }
    }
}