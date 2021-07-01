using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceTabViewModels.ChoiceViewModelItems
{
    /// <summary>
    /// Вид одежды
    /// </summary>
    public class ChoiceClothesTypeViewModelItem : ChoiceBaseViewModelItem
    {
        public ChoiceClothesTypeViewModelItem(IClothesTypeDomain clothesType)
        {
            ClothesType = clothesType;
        }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public IClothesTypeDomain ClothesType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesTypeName => 
            ClothesType.Name;
    }
}