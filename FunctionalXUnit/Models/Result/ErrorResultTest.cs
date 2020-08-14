using System;
using System.Linq;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

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

            var resultError = error.ToResultValue<string>();

            Assert.True(resultError.HasErrors);
            Assert.Equal(1, resultError.Errors.Count);
        }

        /// <summary>
        /// Преобразование в множество с одним элементом
        /// </summary>
        [Fact]
        public void IsEnumrableType_HasOne()
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

            Assert.Equal(error.ErrorResultType.ToString(), error.ToString());
        }
    }
}