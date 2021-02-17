using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис группы размера одежды
    /// </summary>
    public class SizeGroupRestService : RestServiceBase<int, ISizeGroupDomain, SizeGroupFullTransfer>, ISizeGroupRestService
    {
        public SizeGroupRestService(ISizeGroupApiService sizeGroupApiService,
                                    ISizeGroupTransferConverter sizeGroupTransferConverter)
            : base(sizeGroupApiService, sizeGroupTransferConverter)
        { }
    }
}