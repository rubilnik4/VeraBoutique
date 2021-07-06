using System.Collections.Generic;
using System.Reactive;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.Views.Clothes.Clothes.ViewItems;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Цвет для фильтрации
    /// </summary>
    public class FilterColorViewModelItem: FilterCheckViewModelItem
    {
        public FilterColorViewModelItem(IColorDomain color,
                                        ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand)
            :base(clothesFilterCommand)
        {
            Color = color;
        }

        /// <summary>
        /// Цвет одежды. Доменная модель
        /// </summary>
        public IColorDomain Color { get; }

        /// <summary>
        /// Цвет одежды
        /// </summary>
        public string ColorName =>
            Color.Name;
    }
}