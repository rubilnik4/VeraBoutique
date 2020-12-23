using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Factory.Rest;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using GenderTransfer = BoutiqueDTO.Models.Implementations.Clothes.GenderTransfer;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase
{
    /// <summary>
    /// Загрузка данных типа пола
    /// </summary>
    public static class GenderUpload
    {
        /// <summary>
        /// Загрузить типы пола
        /// </summary>
        public static async Task<IResultError> Upload(IHostConnection hostConnection, ILogger logger) =>
            await new ResultValue<IGenderApiService>(GenderApiService.GetGenderApiService(hostConnection)).
            ResultValueVoidOk(_ => logger.ShowMessage("Загрузка типа пола в базу")).
            ResultValueBindOkAsync(api => api.Post(GenderTransfers.First())).
            ResultValueOkBadTaskAsync(genderType => genderType.
                                                    Void(_ => logger.ShowMessage("Загрузка типа пола завершена успешно")),
                                      errors => GenderType.Male.
                                                Void(_ => logger.ShowMessage("Ошибка загрузки типа пола")).
                                                Void(_ => logger.ShowErrors(errors)));

        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private static IGenderTransferConverter GenderTransferConverter =>
            new GenderTransferConverter();

        /// <summary>
        /// Типы пола
        /// </summary>
        private static IEnumerable<GenderTransfer> GenderTransfers =>
            GenderTransferConverter.ToTransfers(GenderInitialize.Genders);
    }
}