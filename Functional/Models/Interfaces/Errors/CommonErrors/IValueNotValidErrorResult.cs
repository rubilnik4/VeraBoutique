using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка неверного значения
    /// </summary>
    public interface IValueNotValidErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}