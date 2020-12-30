using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    public class ColorRestService : RestServiceBase<string, IColorDomain, ColorTransfer>, IColorRestService
    {
        public ColorRestService(IColorApiService colorApiService, 
                                IColorTransferConverter colorTransferConverter,
                                ILogger logger)
            : base(colorApiService, colorTransferConverter, logger)
        { }
    }
}