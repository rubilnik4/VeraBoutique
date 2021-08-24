namespace Functional.Models.Enums
{
    /// <summary>
    /// Ошибки базы данных
    /// </summary>
    public enum DatabaseErrorType
    {
        DatabaseIncorrectConnection,
        DatabaseSave,
        DatabaseTableAccess,
        DatabaseValueDuplicate,
        DatabaseValueNotFound,
    }
}