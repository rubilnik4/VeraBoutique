using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки типа пола в базу данных
    /// </summary>
    public class GenderUploadService : UploadServiceBase<GenderType, IGenderDomain, GenderTransfer>, IGenderUploadService
    {
        public GenderUploadService(IGenderTransferConverter genderTransferConverter, 
                                   IGenderApiService genderApiService, ILogger logger)
            :base(genderTransferConverter, genderApiService, logger)
        { }
    }
}