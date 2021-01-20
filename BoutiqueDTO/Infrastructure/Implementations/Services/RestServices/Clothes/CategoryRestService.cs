﻿using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис категорий одежды
    /// </summary>
    public class CategoryRestService : RestServiceBase<string, ICategoryDomain, CategoryTransfer>, ICategoryRestService
    {
        public CategoryRestService(ICategoryApiService categoryApiService, 
                                   ICategoryTransferConverter categoryTransferConverter,
                                   IBoutiqueLogger boutiqueLogger)
            : base(categoryApiService, categoryTransferConverter, boutiqueLogger)
        { }
    }
}