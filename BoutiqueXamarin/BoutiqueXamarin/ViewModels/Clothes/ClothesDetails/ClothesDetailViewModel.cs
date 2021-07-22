using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Images;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails
{
    public class ClothesDetailViewModel : NavigationBaseViewModel<ClothesDetailNavigationParameters, IClothesDetailNavigationService>
    {
        public ClothesDetailViewModel(IClothesRestService clothesRestService,
                                      IClothesDetailNavigationService clothesDetailNavigationService)
            : base(clothesDetailNavigationService)
        {
            _clothesDetail = this.WhenAnyValue(x => x.NavigationParameters).
                                  WhereNotNull().
                                  Select(GetClothes).
                                  ToProperty(this, nameof(ClothesDetail));

            _clothesDetailImageViewModelItems = this.WhenAnyValue(x => x.ClothesDetail).
                                                     WhereNotNull().
                                                     SelectMany(clothes => GetClothesImages(clothesRestService, clothes.Id)).
                                                     ToProperty(this, nameof(ClothesDetailImageViewModelItems), scheduler: RxApp.MainThreadScheduler);

            _name = this.WhenAnyValue(x => x.ClothesDetail).
                         WhereNotNull().
                         Select(clothes => clothes.Name).
                         ToProperty(this, nameof(Name));
            _description = this.WhenAnyValue(x => x.ClothesDetail).
                                WhereNotNull().
                                Select(clothes => clothes.Description).
                                ToProperty(this, nameof(Description));
            _price = this.WhenAnyValue(x => x.ClothesDetail).
                          WhereNotNull().
                          Select(clothes => clothes.Price).
                          ToProperty(this, nameof(Price));
            _sizes = this.WhenAnyValue(x => x.ClothesDetail).
                          WhereNotNull().
                          Select(clothes => clothes.SizeGroups).
                          ToProperty(this, nameof(Sizes));
            _colors = this.WhenAnyValue(x => x.ClothesDetail).
                           WhereNotNull().
                           Select(clothes => clothes.Colors).
                           ToProperty(this, nameof(Colors));
        }

        /// <summary>
        /// Наименование
        /// </summary>
        private readonly ObservableAsPropertyHelper<IClothesDetailDomain> _clothesDetail;

        /// <summary>
        /// Наименование
        /// </summary>
        public IClothesDetailDomain ClothesDetail =>
            _clothesDetail.Value;

        /// <summary>
        /// Наименование
        /// </summary>
        private readonly ObservableAsPropertyHelper<string> _name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            _name.Value;

        /// <summary>
        /// Описание
        /// </summary>
        private readonly ObservableAsPropertyHelper<string> _description;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description =>
            _description.Value;

        /// <summary>
        /// Изображения
        /// </summary>
        private readonly ObservableAsPropertyHelper<decimal> _price;

        /// <summary>
        /// Изображения
        /// </summary>
        public decimal Price =>
            _price.Value;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ISizeGroupMainDomain>> _sizes;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<ISizeGroupMainDomain> Sizes =>
            _sizes.Value;

        /// <summary>
        /// Цвета одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<IColorDomain>> _colors;

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<IColorDomain> Colors =>
            _colors.Value;

        /// <summary>
        /// Изображения
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ClothesDetailImageViewModelItem>> _clothesDetailImageViewModelItems;

        /// <summary>
        /// Изображения
        /// </summary>
        public IReadOnlyCollection<ClothesDetailImageViewModelItem> ClothesDetailImageViewModelItems =>
            _clothesDetailImageViewModelItems.Value;

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        private static IClothesDetailDomain GetClothes(ClothesDetailNavigationParameters? clothesDetailsParameters) =>
            clothesDetailsParameters.ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound, nameof(ClothesDetailNavigationParameters))).
            ResultValueOk(parameters => parameters.ClothesDetail).
            Map(result => result.Value);

        /// <summary>
        /// Получить модели изображений
        /// </summary>
        private static async Task<IReadOnlyCollection<ClothesDetailImageViewModelItem>> GetClothesImages(IClothesRestService clothesRestService, 
                                                                                                         int clothesId) =>
            await clothesRestService.GetImage(clothesId).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   _ => new byte[0]).
            MapTaskAsync(ImageConverter.ToImageSource).
            MapTaskAsync(imageSource => new ClothesDetailImageViewModelItem(imageSource)).
            MapTaskAsync(clothesDetailImage => new List<ClothesDetailImageViewModelItem> { clothesDetailImage });
    }
}