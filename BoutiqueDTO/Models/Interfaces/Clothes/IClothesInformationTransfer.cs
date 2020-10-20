using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public interface IClothesInformationTransfer: IClothesInformation, IClothesShortTransfer
    {
        
    }
}