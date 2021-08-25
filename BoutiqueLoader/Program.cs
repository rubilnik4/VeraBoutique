﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueLoader.Factories.Configuration;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
using BoutiqueLoader.Factories.Logger;
using BoutiqueLoader.Factories.Services;
using BoutiqueLoader.Infrastructure.Implementations;
using BoutiqueLoader.Infrastructure.Implementations.Logger;
using BoutiqueLoader.Infrastructure.Implementations.Services.Upload;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;

namespace BoutiqueLoader
{
    public class Program
    {
        private static IBoutiqueLogger BoutiqueLogger => LoggerFactory.BoutiqueLogger;

        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await BoutiqueUpload.UploadAuthorizeData(BoutiqueLogger);
    }
}
