using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка отсутствующего значения
    /// </summary>
    public interface IValueNotFoundErrorResult: IErrorBaseResult<CommonErrorType>
    { }
}