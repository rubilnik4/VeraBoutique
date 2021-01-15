using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис цвета одежды
    /// </summary>
    public class ColorRestService : RestServiceBase<string, IColorDomain, ColorTransfer>, IColorRestService
    {
        public ColorRestService(IColorApiService colorApiService, 
                                IColorTransferConverter colorTransferConverter,
                                IBoutiqueLogger boutiqueLogger)
            : base(colorApiService, colorTransferConverter, boutiqueLogger)
        { }
    }
}