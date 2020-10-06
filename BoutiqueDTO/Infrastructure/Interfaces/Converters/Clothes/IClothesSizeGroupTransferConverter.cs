using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public interface IClothesSizeGroupTransferConverter : ITransferConverter<string, IClothesSizeGroupDomain, ClothesSizeGroupTransfer>
    { }
}