using System.IO;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модель одежды
    /// </summary>
    public class ClothesViewModelItem
    {
        public ClothesViewModelItem(IClothesDomain clothesDomain)
        {
            _clothesDomain = clothesDomain;
        }

        /// <summary>
        /// Одежда. Доменная модель
        /// </summary>
        private readonly IClothesDomain _clothesDomain;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            _clothesDomain.Name;

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price =>
            _clothesDomain.Price;

        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource ImageSource =>
            ImageSource.FromStream(() => new MemoryStream(_clothesDomain.Image));
    }
}