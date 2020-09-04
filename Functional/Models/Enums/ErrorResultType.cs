namespace Functional.Models.Enums
{
    /// <summary>
    /// Тип ошибки результирующего ответа
    /// </summary>
    public enum ErrorResultType
    {
        DevideByZero,
        DatabaseIncorrectConnection,
        DatabaseSave,
        DatabaseTableAccess,
        JsonConvertion,
        Unknown,
    }
}