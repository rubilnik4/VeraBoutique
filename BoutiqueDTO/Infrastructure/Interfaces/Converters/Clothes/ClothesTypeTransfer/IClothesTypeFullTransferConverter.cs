using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer
{
    /// <summary>
    /// Конвертер полной информации вида одежды в трансферную модель
    /// </summary>
    public interface IClothesTypeFullTransferConverter : 
        ITransferConverter<string, IClothesTypeFullDomain, ClothesTypeFullTransfer>
    { }
}