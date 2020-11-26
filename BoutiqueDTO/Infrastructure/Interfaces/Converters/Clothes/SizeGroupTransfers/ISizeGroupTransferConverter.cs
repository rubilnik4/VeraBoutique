using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public interface ISizeGroupTransferConverter : ITransferConverter<(ClothesSizeType, int),
                                                   ISizeGroupDomain, SizeGroupTransfer>
    { }
}