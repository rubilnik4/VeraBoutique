using System;
using System.Threading.Channels;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Models.Mocks.Interfaces;
using Moq;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок
        /// </summary>
        [Fact]
        public void ResultVoid_Ok_CallVoid()
        {
            var resultOk = new ResultValue<string>("test");
            var voidObjectMock = new Mock<IVoidObject>();
           
            var resultAfterVoid = resultOk.ResultVoid(voidObjectMock.Object.TestVoid);

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultVoid_Bad_CallVoid()
        {
            var resultBad = new ResultValue<string>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultBad.ResultVoid(voidObjectMock.Object.TestVoid);

            Assert.True(resultAfterVoid.Equals(resultBad));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

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
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void ResultVoidOk_Bad_NotCallVoid()
        {
            var resultOk = new ResultValue<int>(CreateErrorTest());
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultVoidOk(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void ResultVoidBad_Ok_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultOk = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(0), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void ResultVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultOk = new ResultValue<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(errorsInitial.Count), Times.Once);
        }
    }
}