using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Models.Implementation.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;

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
        public static GenderDto ToDto(IGenderDomain gender) =>
            new GenderDto(gender.GenderType, gender.Name);

        /// <summary>
        /// Преобразовать коллекцию полов в трансферную модель
        /// </summary>
        public static IEnumerable<GenderDto> ToDtoCollection(IEnumerable<IGenderDomain> genders) =>
            genders.Select(ToDto);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public static IGenderDomain FromDto(GenderDto genderDto) =>
            new GenderDomain(genderDto.GenderType, genderDto.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public static IEnumerable<IGenderDomain> FromDtoCollection(IEnumerable<GenderDto> gendersDto) =>
            gendersDto.Select(FromDto);

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public static IResultValue<string> ToJson(IGenderDomain gender) =>
            ToDto(gender).
            Map(genderDto => ResultValueTry(() => JsonSerializer.Serialize(genderDto, JsonSettings.CyrillicJsonOptions),
                                            ErrorJsonConverting(nameof(Gender))));

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public static IResultValue<IGenderDomain> FromJson(string genderJson) =>
            ResultValueTry(() => JsonSerializer.Deserialize<GenderDto>(genderJson, JsonSettings.CyrillicJsonOptions),
                           ErrorJsonSchema(nameof(Gender))).
            ResultValueOk(FromDto);

        /// <summary>
        /// Преобразовать коллекцию типа пола в Json
        /// </summary>
        public static IResultValue<string> ToJsonCollection(IEnumerable<IGenderDomain> genders) =>
            genders.Select(ToDto).
            Map(gendersDto => ResultValueTry(() => JsonSerializer.Serialize(gendersDto, JsonSettings.CyrillicJsonOptions),
                                             ErrorJsonConverting(nameof(Gender))));

        /// <summary>
        /// Преобразовать Json в коллекцию
        /// </summary>
        public static IResultCollection<IGenderDomain> FromJsonCollection(string gendersJson) =>
            ResultCollectionTry(() => JsonSerializer.Deserialize<IEnumerable<GenderDto>>(gendersJson, JsonSettings.CyrillicJsonOptions).ToList(),
                                                                                                              ErrorJsonSchema(nameof(Gender))).
            ResultCollectionOk(gendersDto => gendersDto.Select(FromDto));

        /// <summary>
        /// Ошибка преобразования в Json
        /// </summary>
        public static IErrorResult ErrorJsonSchema(string schemaName) =>
            new ErrorResult(ErrorResultType.JsonConvertion, $"Схема Json не соответствует классу {schemaName}");

        /// <summary>
        /// Ошибка преобразования в Json
        /// </summary>
        public static IErrorResult ErrorJsonConverting(string schemaName) =>
            new ErrorResult(ErrorResultType.JsonConvertion, $"Невозможно преобразовать в Json тип {schemaName}");
    }
}