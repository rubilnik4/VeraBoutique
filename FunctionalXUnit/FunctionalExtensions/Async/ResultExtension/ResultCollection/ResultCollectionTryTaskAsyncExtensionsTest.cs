﻿using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.Collections;
using static FunctionalXUnit.Data.ErrorData;
using static FunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollection
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionTryTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialNumbers));

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, CreateErrorTest());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialError));

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, CreateErrorTest());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialNumbers));

            var numbersAfterTry = await numberResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.Equal(ErrorResultType.DevideByZero, numbersAfterTry.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialError));

            var numberAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}