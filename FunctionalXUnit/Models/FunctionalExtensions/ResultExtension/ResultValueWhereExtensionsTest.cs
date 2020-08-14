﻿using System;
using System.Linq;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Implementations.Result;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                resultValue.ResultValueContinue(number => true,
                                                okFunc: number => number.ToString(),
                                                badFunc: _ => CreateErrorTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewError()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var errorBad = CreateErrorTest();
            var resultAfterWhere =
                resultValue.ResultValueContinue(number => false,
                                                okFunc: _ => String.Empty,
                                                badFunc: number => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorBad.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere =
                resultValue.ResultValueContinue(number => true,
                                                okFunc: _ => String.Empty,
                                                badFunc: _ => CreateErrorTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere =
                resultValue.ResultValueContinue(number => false,
                                                okFunc: _ => String.Empty,
                                                badFunc: _ => CreateErrorTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueOkBad_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                resultValue.ResultValueOkBad(
                    okFunc: number => number.ToString(),
                    badFunc: _ => String.Empty);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValueOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere =
                resultValue.ResultValueOkBad(
                    okFunc: _ => String.Empty,
                    badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Errors.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueOk_Ok_ReturnNewValue()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueOk(number => number.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueOk(number => number.ToString());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBad_Ok_ReturnInitial()
        {
            const int initialValue = 2;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBad(errors => errors.Count);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBad(errors => errors.Count);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}