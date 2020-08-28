using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BoutiqueDTO.Infrastructure.Implementation.Converters
{
    /// <summary>
    /// Параметры конвертации в Json
    /// </summary>
    public static class JsonSettings
    {
        /// <summary>
        /// Параметры конвертации в Json
        /// </summary>
        public static JsonSerializerOptions CyrillicJsonOptions =>
            new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
    }
}