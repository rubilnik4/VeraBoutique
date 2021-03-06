﻿using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = new ResultValue<IEnumerable<int>>(collection);

            var resultCollection = resultNoError.ToResultCollection();

            Assert.True(resultCollection.OkStatus);
            Assert.True(collection.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultValue<IEnumerable<int>>(error);

            var resultCollection = resultHasError.ToResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
            Assert.True(error.Equals(resultCollection.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultValues = collection.Select(value => new ResultValue<int>(value));

            var resultCollection = resultValues.ToResultCollection();

            Assert.True(resultCollection.OkStatus);
            Assert.True(collection.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_HasErrors()
        {
            var error = CreateErrorTest();
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultValues = collection.Select(value => new ResultValue<int>(value)).
                                          Append(new ResultValue<int>(error));

            var resultCollection = resultValues.ToResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
            Assert.True(error.Equals(resultCollection.Errors.Last()));
        }

        /// <summary>
        /// Проверить объект. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValue_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValue();

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValueNullCheck(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var resultString = initialString.ToResultValueNullCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }
    }
}