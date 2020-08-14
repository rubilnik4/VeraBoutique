using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using System.Linq;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.Result
{
    /// <summary>
    /// Базовый вариант ответа. Тест
    /// </summary>
    public class ResultErrorTest
    {
        /// <summary>
        /// Базовая инициализация. Отсутствие ошибок
        /// </summary>
        [Fact]
        public void Initialize_Base_OkStatus()
        {
            var resultError = new ResultError();

            Assert.True(resultError.OkStatus);
            Assert.False(resultError.HasErrors);
            Assert.Empty(resultError.Errors);
        }

        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize_Error_HasOne()
        {
            var resultError = new ResultError(CreateErrorTest());

            Assert.False(resultError.OkStatus);
            Assert.True(resultError.HasErrors);
            Assert.Single(resultError.Errors);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultErrorInitial = new ResultError();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcatted = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcatted.HasErrors);
            Assert.Equal(1, resultErrorConcatted.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var resultErrorInitial = new ResultError(CreateErrorTest());
            var errorToConcat = CreateErrorTest();

            var resultErrorConcatted = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcatted.HasErrors);
            Assert.Equal(2, resultErrorConcatted.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcatted.Errors.Last()));
        }
    }
}