﻿using System.Linq;
using BoutiqueMVC.Extensions.Controllers.Sync;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static BoutiqueMVCXUnit.Data.ErrorData;
using static BoutiqueMVCXUnit.Data.Collections;
using System.Collections;
using System.Collections.Generic;

namespace BoutiqueMVCXUnit.Extensions.Controllers.Sync
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера. Тесты
    /// </summary>
    public class ResultValueToActionResultExtensionsTest
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToGetActionResult_OkRequest()
        {
            const int initialNumber = 2;
            var numberResult = new ResultValue<int>(initialNumber);

            var actionResult = numberResult.ToGetActionResult();

            Assert.IsType<OkObjectResult>(actionResult);
            var okRequest = (OkObjectResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, okRequest.StatusCode);
            Assert.Equal(initialNumber, okRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetActionResult_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var actionResult = numberResult.ToGetActionResult();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToGetJsonResult_OkRequest()
        {
            const int initialNumber = 2;
            var numberResult = new ResultValue<int>(initialNumber);

            var actionResult = numberResult.ToGetJsonResult();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.Equal(initialNumber, jsonRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetJsonResult_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var actionResult = numberResult.ToGetJsonResult();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToGetJsonResultCollection_OkRequest()
        {
            var numbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(numbers);

            var actionResult = numbersResult.ToGetJsonResultCollection();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<int>>(jsonRequest.Value);
            Assert.True(numbers.SequenceEqual((IEnumerable<int>)jsonRequest.Value));
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetJsonResultCollection_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var actionResult = numberResult.ToGetJsonResultCollection();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}