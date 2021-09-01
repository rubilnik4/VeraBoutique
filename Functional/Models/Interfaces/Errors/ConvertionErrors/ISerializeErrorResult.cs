using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Errors.ConvertionErrors
{
    /// <summary>
    /// Ошибка сериализации
    /// </summary>
    public interface ISerializeErrorResult: IErrorBaseResult<ConvertionErrorType>
    { }
}