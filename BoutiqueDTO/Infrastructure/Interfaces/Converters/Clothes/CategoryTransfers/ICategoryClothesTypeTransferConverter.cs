using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды с типом в трансферную модель
    /// </summary>
    public interface ICategoryClothesTypeTransferConverter : ITransferConverter<string, ICategoryClothesTypeDomain, CategoryClothesTypeTransfer>
    { }
}