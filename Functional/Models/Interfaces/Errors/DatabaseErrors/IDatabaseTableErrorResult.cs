using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Interfaces.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка таблицы базы данных
    /// </summary>
    public interface IDatabaseTableErrorResult : IErrorBaseResult<DatabaseErrorType>
    { }
}