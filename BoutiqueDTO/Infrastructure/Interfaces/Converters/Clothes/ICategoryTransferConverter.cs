using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public interface ICategoryTransferConverter : ITransferConverter<string, ICategoryDomain, CategoryTransfer>
    { }
}