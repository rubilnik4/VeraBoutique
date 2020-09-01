using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueMVCXUnit.Data
{
    /// <summary>
    /// Инициализация объектов для тестирования
    /// </summary>
    public static class ErrorData
    {
        /// <summary>
        /// Создать тестовый экземпляр ошибки
        /// </summary>
        public static IErrorResult CreateErrorTest() =>
            new ErrorResult(ErrorResultType.Unknown, "Unknown error");

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static IReadOnlyList<IErrorResult> CreateErrorListTwoTest() =>
            new List<IErrorResult>()
            {
                CreateErrorTest(),
                CreateErrorTest(),
            };

        /// <summary>
        /// Создать тестовый экземпляр множества ошибок
        /// </summary>
        public static IEnumerable<IErrorResult> CreateErrorEnumerableTwoTest() =>
            CreateErrorListTwoTest();

        /// <summary>
        /// Создать тестовый экземпляр ошибки в задаче
        /// </summary>
        public static Task<IErrorResult> CreateErrorTestTask() =>
            Task.FromResult(CreateErrorTest());

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static Task<IEnumerable<IErrorResult>> CreateErrorListTwoTestTask() =>
            Task.FromResult(CreateErrorEnumerableTwoTest());
    }
}