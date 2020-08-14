using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.Result
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
            var resultValue = new ResultValue<string>(CreateErrorListTwoTest());

            Assert.False(resultValue.OkStatus);
            Assert.True(resultValue.HasErrors);
            Assert.Equal(2, resultValue.Errors.Count);
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
            Assert.Equal(1, resultValueConcatted.Errors.Count);
            Assert.True(errorToConcat.Equals(resultValueConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var resultValueInitial = new ResultError(CreateErrorTest());
            var errorToConcat = CreateErrorTest();

            var resultValueConcatted = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcatted.HasErrors);
            Assert.Equal(2, resultValueConcatted.Errors.Count);
            Assert.True(errorToConcat.Equals(resultValueConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            var resultValueInitial = new ResultValue<string>("OK");
            var errorsToConcat = Enumerable.Empty<IErrorResult>();

            var resultValueConcatted = resultValueInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcatted.OkStatus);
        }
    }
}