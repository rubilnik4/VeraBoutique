﻿using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа одежды
    /// </summary>
    public interface IClothesTypeApiService : IApiService<string, ClothesTypeTransfer>
    { }
}