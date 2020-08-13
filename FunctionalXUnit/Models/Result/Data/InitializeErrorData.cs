using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using System.Collections.Generic;

namespace FunctionalXUnit.Models.Result.Data
{
    /// <summary>
    /// Инициализация объектов для тестирования
    /// </summary>
    public static class InitializeErrorData
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
    }
}