using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис категорий одежды
    /// </summary>
    public class CategoryRestService : RestServiceBase<string, ICategoryMainDomain, CategoryMainTransfer>, ICategoryRestService
    {
        public CategoryRestService(IRestHttpClient restHttpClient, ICategoryMainTransferConverter categoryMainTransferConverter)
            : base(restHttpClient, categoryMainTransferConverter)
        { }
    }
}