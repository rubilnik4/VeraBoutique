﻿namespace Functional.Models.Enums
{
    /// <summary>
    /// Типы ошибок при авторизации
    /// </summary>
    public enum AuthorizeErrorType
    {
        Username,
        Password,
        Email,
        Phone,
        Token
    }
}