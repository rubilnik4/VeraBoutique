using System.Linq;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Data.Collections;

namespace FunctionalXUnit.Models.Result
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией. Тест
    /// </summary>
    public class ResultCollectionTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultCollection = new ResultCollection<string>(CreateErrorTest());

            Assert.False(resultCollection.OkStatus);
            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<string>(errors);

            Assert.False(resultCollection.OkStatus);
            Assert.True(resultCollection.HasErrors);
            Assert.Equal(errors.Count, resultCollection.Errors.Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var collection = GetRangeNumber();
            var value = new ResultCollection<int>(collection);

            Assert.True(value.OkStatus);
            Assert.False(value.HasErrors);
            Assert.Empty(value.Errors);
            Assert.True(value.Value.SequenceEqual(collection));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultCollectionInitial = new ResultCollection<int>(GetRangeNumber());
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcatted = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcatted.HasErrors);
            Assert.Single(resultCollectionConcatted.Errors);
            Assert.True(errorToConcat.Equals(resultCollectionConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultCollectionInitial = new ResultCollection<int>(initialError);
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcatted = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcatted.HasErrors);
            Assert.Equal(2, resultCollectionConcatted.Errors.Count);
            Assert.True(initialError.Equals(resultCollectionConcatted.Errors.First()));
            Assert.True(errorToConcat.Equals(resultCollectionConcatted.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            var collection = GetRangeNumber();
            var resultCollectionInitial = new ResultCollection<int>(collection);
            var errorsToConcat = Enumerable.Empty<IErrorResult>();

            var resultValueConcatted = resultCollectionInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcatted.OkStatus);
            Assert.True(collection.SequenceEqual(resultValueConcatted.Value));
        }
    }
}