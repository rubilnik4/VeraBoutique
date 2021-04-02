using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using Prism.Commands;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модель отображения одежды
    /// </summary>
    public class ClothesViewModelItem
    {
        public ClothesViewModelItem(IClothesDomain clothesDomain, IClothesDetailNavigationService clothesDetailNavigationService)
        {
            _clothesDomain = clothesDomain;
            _clothesDetailNavigationService = clothesDetailNavigationService;

            ClothesDetailCommand = new DelegateCommand(async () => await ToClothesDetail());
        }

        /// <summary>
        /// Одежда. Доменная модель
        /// </summary>
        private readonly IClothesDomain _clothesDomain;

        /// <summary>
        /// Сервис навигации к странице детализации одежды
        /// </summary>
        private readonly IClothesDetailNavigationService _clothesDetailNavigationService;

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

        /// <summary>
        /// Кнопка перехода на страницу детализации одежды
        /// </summary>
        public DelegateCommand ClothesDetailCommand { get; }

        /// <summary>
        /// Переход на страницу детализации одежды
        /// </summary>
        private async Task ToClothesDetail() =>
            await _clothesDetailNavigationService.NavigateTo(_clothesDomain.Id);
    }
}