﻿using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис категорий одежды
    /// </summary>
    public class CategoryApiService : ApiService<string, CategoryTransfer>, ICategoryApiService
    {
        public CategoryApiService(IRestClient restClient)
        : base(restClient)
        { }
    }
}