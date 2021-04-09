using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис размера одежды
    /// </summary>
    public class SizeRestService : RestServiceBase<int, ISizeDomain, SizeTransfer>, ISizeRestService
    {
        public SizeRestService(IRestHttpClient restHttpClient,
                               ISizeTransferConverter sizeTransferConverter)
            : base(restHttpClient, sizeTransferConverter)
        { }
    }
}