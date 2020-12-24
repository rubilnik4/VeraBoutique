using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Api сервис цвета одежды
    /// </summary>
    public interface IColorApiService : IApiService<string, ColorTransfer>
    { }
}