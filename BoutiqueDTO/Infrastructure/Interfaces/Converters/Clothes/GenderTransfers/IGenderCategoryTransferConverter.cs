using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers
{
    /// <summary>
    /// Конвертер типа пола c категорией в трансферную модель
    /// </summary>
    public interface IGenderCategoryTransferConverter : ITransferConverter<GenderType, IGenderCategoryDomain, GenderCategoryTransfer>
    { }
}