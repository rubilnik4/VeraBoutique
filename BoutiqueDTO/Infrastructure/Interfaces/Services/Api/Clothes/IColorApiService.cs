using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис цвета одежды
    /// </summary>
    public interface IColorApiService : IApiService<string, ColorTransfer>
    { }
}