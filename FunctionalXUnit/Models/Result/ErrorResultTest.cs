using System;
using System.Linq;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.Models.Result
{
    /// <summary>
    /// Ошибка результирующего ответа. Тест
    /// </summary>
    public class ErrorResultTest
    {
        /// <summary>
        /// Вернуть ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResult_HasOneError()
        {
            var error = CreateErrorTest();

            var resultError = error.ToResult();

            Assert.True(resultError.HasErrors);
            Assert.Equal(1, resultError.Errors.Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResultValue_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var resultValue = error.ToResultValue<string>();

            Assert.True(resultValue.HasErrors);
            Assert.Equal(1, resultValue.Errors.Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResultCollection_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var resultCollection = error.ToResultCollection<string>();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(1, resultCollection.Errors.Count);
        }

        /// <summary>
        /// Преобразование в множество с одним элементом
        /// </summary>
        [Fact]
        public void IsEnumerableType_HasOne()
        {
            var error = CreateErrorTest();

            Assert.Single(error);
        }

        /// <summary>
        /// Преобразование в строку выводит тип ошибки
        /// </summary>
        [Fact]
        public void ToStringFormatIsErrorType()
        {
            var error = CreateErrorTest();

            Assert.IsType<ErrorTypeResult<TestErrorType>>(error);
            Assert.Equal(((ErrorTypeResult<TestErrorType>)error).ErrorType.ToString(), error.ToString());
        }

        [Fact]
        public void AppendException()
        {
            var error = CreateErrorTest();
            var exception = new ArgumentException();

            var errorWithException = error.AppendException(exception);

            Assert.True(exception.Equals(errorWithException.Exception));
        }
    }
}