using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueCommon.Infrastructure.Implementation.Errors
{
    /// <summary>
    /// Ошибки конвертеров
    /// </summary>
    public static class ConverterErrors
    {
        /// <summary>
        /// Элемент не найден
        /// </summary>
        public static IErrorResult ValueNotFoundError(string valueName) =>
            new ErrorResult(ErrorResultType.ValueNotFound, $"Элемент {valueName} не найден");
    }
}