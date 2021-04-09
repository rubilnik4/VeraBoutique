using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис группы размера одежды
    /// </summary>
    public class SizeGroupRestService : RestServiceBase<int, ISizeGroupMainDomain, SizeGroupMainTransfer>, ISizeGroupRestService
    {
        public SizeGroupRestService(IRestHttpClient restHttpClient,
                                    ISizeGroupMainTransferConverter sizeGroupTransferConverter)
            : base(restHttpClient, sizeGroupTransferConverter)
        { }
    }
}