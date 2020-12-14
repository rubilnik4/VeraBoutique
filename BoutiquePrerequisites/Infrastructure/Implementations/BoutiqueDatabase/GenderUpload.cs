using System.Collections.Generic;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
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
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private static IGenderTransferConverter GenderTransferConverter =>
            new GenderTransferConverter();

        //public static IResultError UploadToError(Client boutiqueClient, ILogger logger) =>
        //    new ResultError().
        //    Void(_ => logger.ShowMessage("Загрузка типа пола в базу")).
        //    ResultErrorBindOk(() => boutiqueClient.ApiGenderCollectionAsync(GenderTransfers)).


        //    ;

        private static IEnumerable<GenderTransfer> GenderTransfers =>
            GenderTransferConverter.ToTransfers(GenderInitialize.Genders);
    }
}