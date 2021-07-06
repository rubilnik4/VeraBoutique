using System.Collections.Generic;
using System.Reactive;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модель с выбором
    /// </summary>
    public abstract class FilterCheckViewModelItem: BaseViewModel
    {
        protected FilterCheckViewModelItem(ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand)
        {
            ClothesFilterCommand = clothesFilterCommand;
        }

        /// <summary>
        /// Команда обновления фильтрации одежды
        /// </summary>
        public ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> ClothesFilterCommand { get; }

        /// <summary>
        /// Выбор
        /// </summary>
        public bool IsChecked { get; set; }
    }
}