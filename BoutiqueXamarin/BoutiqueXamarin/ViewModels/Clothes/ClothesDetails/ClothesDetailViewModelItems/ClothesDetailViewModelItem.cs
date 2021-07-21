using System.IO;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems
{
    /// <summary>
    /// Модель одежды
    /// </summary>
    public class ClothesDetailViewModelItem
    {
        public ClothesDetailViewModelItem(IClothesMainDomain clothesMainDomain)
        {
            _clothesMainDomain = clothesMainDomain;
        }

        /// <summary>
        /// Одежда. Детальная информация
        /// </summary>
        private readonly IClothesMainDomain _clothesMainDomain;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            _clothesMainDomain.Name;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description =>
            _clothesMainDomain.Description;

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price =>
            _clothesMainDomain.Price;

        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource ImageSource =>
            ImageSource.FromStream(() => new MemoryStream(_clothesMainDomain.Image));
    }
}