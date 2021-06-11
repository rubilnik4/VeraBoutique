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
            _clothesType = clothesType;
        }

        /// <summary>
        /// Тип одежды
        /// </summary>
        private readonly IClothesTypeDomain _clothesType;

        /// <summary>
        /// Тип одежды
        /// </summary>
        public string ClothesTypeName => 
            _clothesType.Name;
    }
}