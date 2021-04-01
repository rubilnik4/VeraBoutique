using System.Collections.Generic;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Колонки одежды
    /// </summary>
    public class ClothesViewModelColumnItem
    {
        public ClothesViewModelColumnItem(ClothesViewModelItem? clothesViewModelItemLeft,
                                          ClothesViewModelItem? clothesViewModelItemRight)
        {
            ClothesViewModelItemLeft = clothesViewModelItemLeft;
            ClothesViewModelItemRight = clothesViewModelItemRight;
        }

        /// <summary>
        /// Одежда. Левая колонка
        /// </summary>
        public ClothesViewModelItem? ClothesViewModelItemLeft { get; }

        /// <summary>
        /// Одежда. Правая колонка
        /// </summary>
        public ClothesViewModelItem? ClothesViewModelItemRight { get; }

        /// <summary>
        /// Одежда
        /// </summary>
        public IReadOnlyCollection<ClothesViewModelItem?> ClothesViewModelItems =>
            new List<ClothesViewModelItem?> { ClothesViewModelItemLeft, ClothesViewModelItemRight };
    }
}