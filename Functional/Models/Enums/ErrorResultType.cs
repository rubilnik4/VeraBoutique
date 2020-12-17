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
        ValueNotFound,
        ValueNotValid,
        CollectionEmpty,
        DatabaseValueDuplicate,
        JsonConvertion,
        UserDefaultNotFound,
        Unknown,
        
        BadGateway,
        BadRequest,
        GatewayTimeout,
        InternalServerError,
        RequestTimeOut,
        Unauthorized,
    }
}