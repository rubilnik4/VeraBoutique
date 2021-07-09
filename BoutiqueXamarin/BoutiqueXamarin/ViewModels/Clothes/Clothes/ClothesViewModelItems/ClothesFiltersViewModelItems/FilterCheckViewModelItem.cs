using System.Collections.Generic;
using System.Reactive;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems
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
        private bool _isChecked;

        /// <summary>
        /// Выбор
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }
    }
}