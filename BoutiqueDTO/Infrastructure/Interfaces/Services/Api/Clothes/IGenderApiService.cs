using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public interface IGenderApiService: IApiService<GenderType, GenderTransfer>
    { }
}