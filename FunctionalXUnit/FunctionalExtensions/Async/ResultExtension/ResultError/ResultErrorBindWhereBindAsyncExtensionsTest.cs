﻿using System.Linq;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultError
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа для задачи-объекта. Тест
    /// </summary>
    public class ResultErrorBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Ok_NoError()
        {
            var initialResult = Task.FromResult((IResultError)new Functional.Models.Implementations.Result.ResultError());
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError();

            var result = await initialResult.ResultErrorBindOkBindAsync(() => Task.FromResult(addingResult));

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = Task.FromResult((IResultError)new Functional.Models.Implementations.Result.ResultError());
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkBindAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = Task.FromResult((IResultError)new Functional.Models.Implementations.Result.ResultError(initialError));
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError();

            var result = await initialResult.ResultErrorBindOkBindAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult.Result));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = Task.FromResult((IResultError)new Functional.Models.Implementations.Result.ResultError(initialError));
            var addingResult = (IResultError)new Functional.Models.Implementations.Result.ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkBindAsync(() => Task.FromResult(addingResult));

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult.Result));
        }
    }
}