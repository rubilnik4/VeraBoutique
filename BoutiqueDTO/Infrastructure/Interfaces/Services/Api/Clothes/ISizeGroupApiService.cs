using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис группы размеров одежды
    /// </summary>
    public interface ISizeGroupApiService : IApiService<int, SizeGroupMainTransfer>
    { }
}