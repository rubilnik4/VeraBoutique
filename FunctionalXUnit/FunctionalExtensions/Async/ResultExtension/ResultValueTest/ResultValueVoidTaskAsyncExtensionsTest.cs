using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением задачей-объектом.Тесты
    /// </summary>
    public class ResultCollectionVoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkTaskAsync_Ok_CallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.
                                  ResultValueVoidOkTaskAsync(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkTaskAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultValueVoidOkTaskAsync(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadTaskAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultValueVoidBadTaskAsync(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadTaskAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.
                                  ResultValueVoidBadTaskAsync(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Ok_OkPredicate_CallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOkTask.
                ResultValueVoidOkWhereTaskAsync(number => true,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Ok_BadPredicate_NotCallVoid()
        {
            const int initialNumber = 1;
            var resultOkTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultOkTask.
                ResultValueVoidOkWhereTaskAsync(number => false,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialNumber, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultErrorTask.
                ResultValueVoidOkWhereTaskAsync(number => true,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = Task.FromResult((IResultValue<int>)new ResultValue<int>(errorsInitial));
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid =
                await resultErrorTask.
                ResultValueVoidOkWhereTaskAsync(number => false,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }
    }
}