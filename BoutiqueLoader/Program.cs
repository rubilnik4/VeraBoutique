using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueLoader.Factories.Configuration;
using BoutiqueLoader.Factories.Connection;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
using BoutiqueLoader.Factories.Logger;
using BoutiqueLoader.Factories.Services;
using BoutiqueLoader.Infrastructure.Implementations;
using BoutiqueLoader.Infrastructure.Implementations.Logger;
using BoutiqueLoader.Infrastructure.Implementations.Services.Upload;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueLoader
{
    public class Program
    {
        private static IBoutiqueLogger BoutiqueLogger => LoggerFactory.BoutiqueLogger;
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await LoaderConfigurationFactory.GetConfiguration(BoutiqueLogger).
           ResultValueBindErrorsOkBindAsync(configuration => BoutiqueUpload.UploadAuthorizeData(configuration, BoutiqueLogger));

       
    }
}
