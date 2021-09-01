using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Errors.ConvertionErrors
{
    /// <summary>
    /// Ошибка десериализации
    /// </summary>
    public interface IDeserializeErrorResult : IErrorBaseResult<ConvertionErrorType>
    { }
}