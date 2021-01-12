using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    public class CategoryRestService : RestServiceBase<string, ICategoryDomain, CategoryTransfer>, ICategoryRestService
    {
        public CategoryRestService(ICategoryApiService categoryApiService, 
                                   ICategoryTransferConverter categoryTransferConverter,
                                   ILogger logger)
            : base(categoryApiService, categoryTransferConverter, logger)
        { }
    }
}