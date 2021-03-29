using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice.ChoiceViewModelItems
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
        /// Вид одежды
        /// </summary>
        private readonly IClothesTypeDomain _clothesType;
    }
}