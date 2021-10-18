namespace BoutiqueDTO.Infrastructure.Implementations.Scheme
{
    /// <summary>
    /// Схемы ответа сервера
    /// </summary>
    public static class JsonSchemas
    {
        /// <summary>
        /// Схема. Некорректный запрос
        /// </summary>
        public static string BadRequestJsonSchema =>
          @"{
              'type': 'object',
              'properties': {
                  'traceId': {'type':'string', 'required': true},
                  'status': {'type': 'integer', 'required': true},
                  'title': {'type': 'string', 'required': true},
                  'type': {'type': 'string', 'required': true},
              }
            }";
    }
}