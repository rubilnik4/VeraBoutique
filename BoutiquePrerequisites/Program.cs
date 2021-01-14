using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.Connection;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Factories.Services;
using BoutiquePrerequisites.Infrastructure.Implementations;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase;
using BoutiquePrerequisites.Infrastructure.Implementations.Logger;
using BoutiquePrerequisites.Infrastructure.Implementations.Services.Upload;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiquePrerequisites
{
    public class Program
    {
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await new ConsoleBoutiqueLogger().
           MapAsync(BoutiqueUpload.UploadAuthorizeData);
    }
}
