using System;
using System.Linq;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Xunit;
using static FunctionalXUnit.Models.Data.ErrorData;

namespace FunctionalXUnit.Models.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class ResultValueCurryExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddOkStatus()
        {
            var resultValueFunc = new ResultValue<Func<int, string>>(IntToString);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal("2", resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddBadStatus()
        {
            var resultValueFunc = new ResultValue<Func<int, string>>(IntToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddOkStatus_TwoArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, string>>(AggregateTwoToString);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal("4", resultOut.Value.Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, string>>(AggregateTwoToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddOkStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddOkStatus_ThreeArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(AggregateThreeToString);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal("6", resultOut.Value.Invoke(2, 2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(AggregateThreeToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddOkStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultCurryOkBind_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultCurryOkBind(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразовать число в строку
        /// </summary>
        private static string IntToString(int number) => number.ToString();

        /// <summary>
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        private static string AggregateTwoToString(int first, int second) => (first + second).ToString();

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        private static string AggregateThreeToString(int first, int second, int third) => (first + second + third).ToString();
    }
}