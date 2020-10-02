using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public interface ICategoryTransferConverter : ITransferConverter<string, ICategoryDomain, CategoryTransfer>
    { }
}