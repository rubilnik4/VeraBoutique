using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public interface ICategoryMainTransferConverter : ITransferConverter<string, ICategoryMainDomain, CategoryMainTransfer>
    { }
}