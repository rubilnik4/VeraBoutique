using System.Collections.Generic;
using System.Linq;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением. Тест
    /// </summary>
    public class ResultValueTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultValue = new ResultValue<string>(CreateErrorTest());

            Assert.False(resultValue.OkStatus);
            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            Assert.False(resultValue.OkStatus);
            Assert.True(resultValue.HasErrors);
            Assert.Equal(errors.Count, resultValue.Errors.Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var resultValue = new ResultValue<string>("OK");

            Assert.True(resultValue.OkStatus);
            Assert.False(resultValue.HasErrors);
            Assert.Empty(resultValue.Errors);
            Assert.True(resultValue.Value != null);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultValueInitial = new ResultValue<string>("OK");
            var errorToConcat = CreateErrorTest();

            var resultValueConcatted = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcatted.HasErrors);
            Assert.Single(resultValueConcatted.Errors);
            Assert.True(errorToConcat.Equals(resultValueConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultValueInitial = new ResultValue<string>(initialError);
            var errorToConcat = CreateErrorTest();

            var resultValueConcatted = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcatted.HasErrors);
            Assert.Equal(2, resultValueConcatted.Errors.Count);
            Assert.True(initialError.Equals(resultValueConcatted.Errors.First()));
            Assert.True(errorToConcat.Equals(resultValueConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            string valueInitial = "OK";
            var resultValueInitial = new ResultValue<string>(valueInitial);
            var errorsToConcat = Enumerable.Empty<IErrorResult>();

            var resultValueConcatted = resultValueInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcatted.OkStatus);
            Assert.Equal(valueInitial, resultValueConcatted.Value);
        }
    }
}