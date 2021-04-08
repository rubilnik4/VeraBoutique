using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.NotifyTasks;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Prism.Commands;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модель отображения одежды
    /// </summary>
    public class ClothesViewModelItem
    {
        public ClothesViewModelItem(IClothesDomain clothesDomain, IClothesRestService clothesRestService,
                                    IClothesDetailNavigationService clothesDetailNavigationService)
        {
            _clothesDomain = clothesDomain;
            _clothesDetailNavigationService = clothesDetailNavigationService;
            Image = new NotifyResultTask<ImageSource>(GetImageSource(clothesRestService, _clothesDomain.Id),
                                                      ImageSource.FromStream(() => new MemoryStream(new byte[0])));
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
        public NotifyResultTask<ImageSource> Image { get; }

        /// <summary>
        /// Кнопка перехода на страницу детализации одежды
        /// </summary>
        public DelegateCommand ClothesDetailCommand { get; }

        /// <summary>
        /// Переход на страницу детализации одежды
        /// </summary>
        private async Task ToClothesDetail() =>
            await _clothesDetailNavigationService.NavigateTo(_clothesDomain.Id);

        /// <summary>
        /// Преобразовать изображение в поток
        /// </summary>
        private static async Task<IResultValue<ImageSource>> GetImageSource(IClothesRestService clothesRestService, int clothesId) =>
            await clothesRestService.GetImageAsync(clothesId).
            ResultValueOkTaskAsync(image => ImageSource.FromStream(() => new MemoryStream(image)));
    }
}