﻿using System;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using FunctionalXUnit.Data;
using Xunit;
using static FunctionalXUnit.Data.ErrorData;

namespace FunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueContinue(number => true,
                                                                   okFunc: number => number.ToString(),
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueContinue(number => false,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: number => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueContinue(number => true,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueContinue(number => false,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueOkBad(okFunc: number => number.ToString(),
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
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueOkBad(okFunc: _ => String.Empty,
                                                                badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
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
            int initialValue = Numbers.Number;
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