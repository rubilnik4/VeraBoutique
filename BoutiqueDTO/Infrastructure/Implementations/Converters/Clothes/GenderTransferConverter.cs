using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection.ResultCollectionTryExtensions;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class GenderTransferConverter: IGenderTransferConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public IGenderTransfer ToTransfer(IGenderDomain gender) =>
            new GenderTransfer(gender.GenderType, gender.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public  IGenderDomain FromTransfer(IGenderTransfer genderTransfer) =>
            new GenderDomain(genderTransfer.GenderType, genderTransfer.Name);

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public IResultValue<string> ToJson(IGenderDomain gender) =>
            ToTransfer(gender).
            Map(genderTransfer => ResultValueTry(() => JsonSerializer.Serialize(genderTransfer, JsonSettings.CyrillicJsonOptions),
                                                 ErrorJsonConverting(nameof(Gender))));

        /// <summary>
        /// Преобразовать тип пола в Json
        /// </summary>
        public IResultValue<IGenderDomain> FromJson(string genderJson) =>
            ResultValueTry(() => JsonSerializer.Deserialize<GenderTransfer>(genderJson, JsonSettings.CyrillicJsonOptions),
                           ErrorJsonSchema(nameof(Gender))).
            ResultValueOk(FromTransfer);

        /// <summary>
        /// Преобразовать коллекцию типа пола в Json
        /// </summary>
        public IResultValue<string> ToJsonCollection(IEnumerable<IGenderDomain> gender) =>
            gender.Select(ToTransfer).
            Map(gendersDto => ResultValueTry(() => JsonSerializer.Serialize(gendersDto, JsonSettings.CyrillicJsonOptions),
                                             ErrorJsonConverting(nameof(Gender))));

        /// <summary>
        /// Преобразовать Json в коллекцию
        /// </summary>
        public IResultCollection<IGenderDomain> FromJsonCollection(string gendersJson) =>
            ResultCollectionTry(() => JsonSerializer.Deserialize<IEnumerable<GenderTransfer>>(gendersJson, JsonSettings.CyrillicJsonOptions).ToList(),
                                                                                                              ErrorJsonSchema(nameof(Gender))).
            ResultCollectionOk(genderTransfers => genderTransfers.Select(FromTransfer));

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