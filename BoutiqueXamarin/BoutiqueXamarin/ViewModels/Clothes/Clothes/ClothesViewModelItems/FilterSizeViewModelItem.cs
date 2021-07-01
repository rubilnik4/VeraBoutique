using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    public class FilterSizeViewModelItem: ReactiveObject
    {
        public FilterSizeViewModelItem(ISizeDomain sizeDomain)
        {
            _sizeDomain = sizeDomain;
        }

        /// <summary>
        /// Размер одежды
        /// </summary>
        private readonly ISizeDomain _sizeDomain;

        /// <summary>
        /// Наименование размера
        /// </summary>
        public string SizeName =>
            _sizeDomain.SizeNameShort;
    }
}