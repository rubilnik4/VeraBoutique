namespace Functional.Models.Enums
{
    /// <summary>
    /// Тип ошибки результирующего ответа
    /// </summary>
    public enum ErrorResultType
    {
        DivideByZero,
        DatabaseIncorrectConnection,
        DatabaseSave,
        DatabaseTableAccess,
        DatabaseValueNotFound,
        DatabaseValueDuplicate,
        JsonConvertion,
        UserDefaultNotFound,
        Unknown,
    }
}