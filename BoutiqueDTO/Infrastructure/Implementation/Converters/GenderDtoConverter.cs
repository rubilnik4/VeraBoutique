using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDTO.Models.Implementation.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDTO.Infrastructure.Implementation.Converters
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public static class GenderDtoConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public static GenderDto ToDto(Gender gender) =>
            new GenderDto(gender.GenderType, gender.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public static Gender FromDto(GenderDto genderDto) =>
            new Gender(genderDto.GenderType, genderDto.Name);

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public static string ToJson(Gender gender) =>
            ToDto(gender).
            Map(genderDto => JsonSerializer.Serialize(genderDto));

        /// <summary>
        /// Преобразовать коллекцию типа пола в Json
        /// </summary>
        public static string ToJsonCollection(IEnumerable<Gender> genders) =>
            genders.Select(ToDto).
            Map(gendersDto => JsonSerializer.Serialize(gendersDto, JsonSettings.CyrillicJsonOptions));
    }
}