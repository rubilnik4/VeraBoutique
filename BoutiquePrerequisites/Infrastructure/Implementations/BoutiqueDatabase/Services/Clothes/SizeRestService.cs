using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки размера одежды в базу данных
    /// </summary>
    public class SizeRestService : RestServiceBase<(SizeType, string), ISizeDomain, SizeTransfer>, ISizeRestService
    {
        public SizeRestService(ISizeApiService sizeApiService,
                               ISizeTransferConverter sizeTransferConverter,
                               ILogger logger)
            : base(sizeApiService, sizeTransferConverter, logger)
        { }
    }
}