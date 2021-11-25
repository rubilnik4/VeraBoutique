using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Images;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.ViewModels.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using Prism.Commands;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модель отображения одежды
    /// </summary>
    public class ClothesViewModelItem: BaseViewModel
    {
        public ClothesViewModelItem(IClothesDetailDomain clothesDetail, SizeType sizeTypeDefault, 
                                    IClothesRestService clothesRestService, IClothesNavigationService clothesNavigationService)
        {
            ClothesDetail = clothesDetail;
            _sizeTypeDefault = sizeTypeDefault;
            _clothesNavigationService = clothesNavigationService;
            _image = Observable.Return(ImageSource.FromFile("empty_image.png")).
                                ToProperty(this, nameof(Image));
            ImageCommand = ReactiveCommand.CreateFromTask(() => GetImageSource(clothesRestService, ClothesDetail.Id));
            _image = ImageCommand.ToProperty(this, nameof(Image), scheduler: RxApp.MainThreadScheduler);
            this.WhenAnyValue(x => x.Image).
                 Where(x => x != null).
                 Subscribe(x => ImageLoad = true);

             ClothesDetailCommand = ReactiveCommand.CreateFromTask(ToClothesDetail);
        }

        /// <summary>
        /// Одежда. Доменная модель
        /// </summary>
        public IClothesDetailDomain ClothesDetail { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        private readonly SizeType _sizeTypeDefault;

        /// <summary>
        /// Навигация к странице одежды
        /// </summary>
        private readonly IClothesNavigationService _clothesNavigationService;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            ClothesDetail.Name;

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price =>
            ClothesDetail.Price;

        /// <summary>
        /// Изображение
        /// </summary>
        private readonly ObservableAsPropertyHelper<ImageSource> _image;

        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource Image => 
            _image.Value;

        /// <summary>
        /// Команда пол
        /// </summary>
        public ReactiveCommand<Unit, ImageSource> ImageCommand { get; }

        /// <summary>
        /// Кнопка перехода на страницу детализации одежды
        /// </summary>
        public ReactiveCommand<Unit, Unit> ClothesDetailCommand { get; }

        /// <summary>
        /// Статус загрузки картинки
        /// </summary>
        public bool ImageLoad { get; private set; }

        /// <summary>
        /// Переход на страницу детализации одежды
        /// </summary>
        private async Task ToClothesDetail() =>
            await _clothesNavigationService.ToClothesDetailPage(ClothesDetail, _sizeTypeDefault);

        /// <summary>
        /// Преобразовать изображение в поток
        /// </summary>
        private static async Task<ImageSource> GetImageSource(IClothesRestService clothesRestService, int clothesId) =>
            await clothesRestService.GetImage(clothesId).
            ResultValueToValueOkBadTaskAsync(bytes => bytes,
                                             _ => Array.Empty<byte>()).
            MapTaskAsync(ImageConverter.ToImageSource);
    }
}