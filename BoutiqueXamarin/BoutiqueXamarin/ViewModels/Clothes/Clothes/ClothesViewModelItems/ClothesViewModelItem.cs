﻿using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.NotifyTasks;
using BoutiqueXamarin.ViewModels.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
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
        public ClothesViewModelItem(IClothesDomain clothesDomain, IClothesRestService clothesRestService,
                                    IClothesDetailNavigationService clothesDetailNavigationService)
        {
            ClothesDomain = clothesDomain;
            _clothesDetailNavigationService = clothesDetailNavigationService;

            _image = Observable.FromAsync(() => GetImageSource(clothesRestService, ClothesDomain.Id)).
                     ToProperty(this, nameof(Image), scheduler: RxApp.MainThreadScheduler);

            ClothesDetailCommand = ReactiveCommand.CreateFromTask(ToClothesDetail);
        }

        /// <summary>
        /// Одежда. Доменная модель
        /// </summary>
        public IClothesDomain ClothesDomain { get; }

        /// <summary>
        /// Сервис навигации к странице детализации одежды
        /// </summary>
        private readonly IClothesDetailNavigationService _clothesDetailNavigationService;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            ClothesDomain.Name;

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price =>
            ClothesDomain.Price;

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
        /// Кнопка перехода на страницу детализации одежды
        /// </summary>
        public ReactiveCommand<Unit, Unit> ClothesDetailCommand { get; }

        /// <summary>
        /// Переход на страницу детализации одежды
        /// </summary>
        private async Task ToClothesDetail() =>
            await _clothesDetailNavigationService.NavigateTo(ClothesDomain.Id);

        /// <summary>
        /// Преобразовать изображение в поток
        /// </summary>
        private static async Task<ImageSource> GetImageSource(IClothesRestService clothesRestService, int clothesId) =>
            await clothesRestService.GetImageAsync(clothesId).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   _ => new byte[0]).
            MapTaskAsync(bytes => ImageSource.FromStream(() => new MemoryStream(bytes)));
    }
}