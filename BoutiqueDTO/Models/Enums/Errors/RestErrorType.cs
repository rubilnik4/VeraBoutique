namespace BoutiqueDTO.Models.Enums.Errors
{
    /// <summary>
    /// Ошибки rest сервиса
    /// </summary>
    public enum RestErrorType
    {
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