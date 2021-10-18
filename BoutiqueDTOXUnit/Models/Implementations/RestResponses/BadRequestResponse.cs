using Newtonsoft.Json;

namespace BoutiqueDTOXUnit.Models.Implementations.RestResponses
{
    /// <summary>
    /// Ответ сервера
    /// </summary>
    public class BadRequestResponse
    {
        [JsonConstructor]
        public BadRequestResponse(string traceId, int status, string title, string type)
        {
            TraceId = traceId;
            Status = status;
            Title = title;
            Type = type;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string TraceId { get; }

        /// <summary>
        /// Статус
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; }
    }
}