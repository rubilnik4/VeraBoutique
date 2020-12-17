﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Clothes;
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
        /// 
        /// </summary>
        public static async Task<IResultError> Upload(IHostConnection hostConnection, ILogger logger) =>
            await new ResultValue<IGenderApiService>(new GenderApiService(hostConnection)).
            ResultValueVoidOk(_ => logger.ShowMessage("Загрузка типа пола в базу")).
            ResultValueBindOkAsync(api => api.Post(GenderTransfers.First())).
            ResultValueVoidOkTaskAsync(_ => logger.ShowMessage("Загрузка типа пола завершена"));

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