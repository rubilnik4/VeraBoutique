using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueMVC.Controllers.Base;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи категорий одежды
    /// </summary>
    public class CategoryController : ApiController<string, ICategoryMainDomain, CategoryMainTransfer>
    {
        public CategoryController(ICategoryDatabaseService categoryDatabaseService,
                                  ICategoryMainTransferConverter categoryTransferConverter)
            : base(categoryDatabaseService, categoryTransferConverter)
        { }
    }
}