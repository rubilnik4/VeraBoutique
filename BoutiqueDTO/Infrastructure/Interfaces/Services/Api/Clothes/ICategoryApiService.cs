using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис категории одежды
    /// </summary>
    public interface ICategoryApiService : IApiService<string, CategoryTransfer>
    { }
}