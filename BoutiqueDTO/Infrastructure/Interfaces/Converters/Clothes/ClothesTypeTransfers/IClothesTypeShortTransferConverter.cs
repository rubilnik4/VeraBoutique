using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер основной информации вида одежды в трансферную модель
    /// </summary>
    public interface IClothesTypeShortTransferConverter :
        ITransferConverter<string, IClothesTypeShortDomain, ClothesTypeShortTransfer>
    { }
}