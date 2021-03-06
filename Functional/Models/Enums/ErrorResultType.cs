﻿namespace Functional.Models.Enums
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
        ConfigurationNotValid,
        Unknown,

        ServerNotFound,
        BadGateway,
        BadRequest,
        GatewayTimeout,
        InternalServerError,
        RequestTimeout,
        RequestEntityToLarge,
        Unauthorized,
        UnsupportedMediaType,
        UnknownRestStatus,
    }
}