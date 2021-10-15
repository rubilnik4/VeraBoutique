namespace BoutiqueDTO.Infrastructure.Implementations.Scheme
{
    /// <summary>
    /// Схемы ответа сервера
    /// </summary>
    public static class JsonSchemes
    {
        /// <summary>
        /// Схема. Некорректный запрос
        /// </summary>
        public static string BadRequestJsonScheme =>
          @"{
              'type': 'object',
              'properties': {
                  'traceId': {'type':'string', 'required': true},
                  'status': {'type': 'integer', 'required': true},
                  'title': {'type': 'string', 'required': true},
                  'type': {'type': 'string', 'required': true},
                  'errors': {'type': 'array', 'required': true},
              }
            }";
    }
}