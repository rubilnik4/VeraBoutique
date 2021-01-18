using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи категорий одежды
    /// </summary>
    public class CategoryController : ApiController<string, ICategoryDomain, CategoryTransfer>
    {
        public CategoryController(ICategoryDatabaseService categoryDatabaseService,
                                  ICategoryTransferConverter categoryTransferConverter)
            : base(categoryDatabaseService, categoryTransferConverter)
        { }
    }
}