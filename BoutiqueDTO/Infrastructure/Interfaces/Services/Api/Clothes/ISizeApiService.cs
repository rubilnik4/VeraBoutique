using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис размера одежды
    /// </summary>
    public interface ISizeApiService : IApiService<(SizeType, string), SizeTransfer>
    { }
}