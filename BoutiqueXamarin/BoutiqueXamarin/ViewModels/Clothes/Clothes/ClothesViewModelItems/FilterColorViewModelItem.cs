using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueXamarin.ViewModels.Base;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Цвет для фильтрации
    /// </summary>
    public class FilterColorViewModelItem: CheckViewModelItem
    {
        public FilterColorViewModelItem(IColorDomain colorDomain )
        {
            _colorDomain = colorDomain;
        }

        /// <summary>
        /// Цвет одежды. Доменная модель
        /// </summary>
        private readonly IColorDomain _colorDomain;

        /// <summary>
        /// Цвет одежды
        /// </summary>
        public string Color =>
            _colorDomain.Name;
    }
}