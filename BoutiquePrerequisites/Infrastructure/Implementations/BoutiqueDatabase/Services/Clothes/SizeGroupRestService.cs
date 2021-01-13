using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки размера одежды в базу данных
    /// </summary>
    public class SizeGroupRestService : RestServiceBase<int, ISizeGroupDomain, SizeGroupTransfer>, ISizeGroupRestService
    {
        public SizeGroupRestService(ISizeGroupApiService sizeGroupApiService,
                                    ISizeGroupTransferConverter sizeGroupTransferConverter,
                                    ILogger logger)
            : base(sizeGroupApiService, sizeGroupTransferConverter, logger)
        { }
    }
}