using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Api сервис типа пола
    /// </summary>
    public interface IGenderApiService: IApiService<GenderType, GenderTransfer>
    { }
}