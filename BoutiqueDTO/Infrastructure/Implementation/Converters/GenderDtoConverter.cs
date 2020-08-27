using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        public static GenderDto ToGenderDto(Gender gender) =>
            new GenderDto()
            {
                GenderType = gender.GenderType,
                Name = gender.Name,
            };

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public static string ToJson(Gender gender) =>
            ToGenderDto(gender).
            Map(genderDto => JsonSerializer.Serialize(genderDto));

        /// <summary>
        /// Преобразовать коллекцию типа пола в Json
        /// </summary>
        public static string ToJsonCollection(IEnumerable<Gender> genders) =>
            genders.Select(ToGenderDto).
            Map(gendersDto => JsonSerializer.Serialize(gendersDto));
    }
}