namespace Functional.Models.Enums
{
    /// <summary>
    /// Ошибки базы данных
    /// </summary>
    public enum DatabaseErrorType
    {
        Connection,
        Save,
        TableAccess,
        ValueDuplicate,
        ValueNotFound,
        ValueNotValid,
    }
}