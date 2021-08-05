using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Images;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
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
            var clothesParameters = this.WhenAnyValue(x => x.NavigationParameters).
                                     WhereNotNull().
                                     Select(GetClothesParameters);

            _clothesDetailImageViewModelItems = clothesParameters.
                                                SelectMany(parameters => Observable.FromAsync(() => GetClothesImages(clothesRestService, parameters.ClothesDetail.Id))).
                                                ToProperty(this, nameof(ClothesDetailImageViewModelItems), scheduler: RxApp.MainThreadScheduler);

            _name = clothesParameters.
                    Select(parameters => parameters.ClothesDetail.Name).
                    ToProperty(this, nameof(Name));
            _description = clothesParameters.
                           Select(parameters => parameters.ClothesDetail.Description).
                           ToProperty(this, nameof(Description));
            _price = clothesParameters.
                     Select(parameters => parameters.ClothesDetail.Price).
                     ToProperty(this, nameof(Price));
            _sizes = clothesParameters.
                     Select(parameters => (IReadOnlyCollection<ISizeGroupDefaultDomain>)parameters.ClothesDetail.SizeGroups.
                                          Select(sizeGroup => new SizeGroupDefaultDomain(sizeGroup, parameters.DefaultSizeType)).
                                          ToList()).
                     ToProperty(this, nameof(Sizes));
            _colors = clothesParameters.
                      Select(parameters => parameters.ClothesDetail.Colors).
                      ToProperty(this, nameof(Colors));
        }

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
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ISizeGroupDefaultDomain>> _sizes;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<ISizeGroupDefaultDomain> Sizes =>
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
        private static ClothesDetailNavigationParameters GetClothesParameters(ClothesDetailNavigationParameters? clothesDetailsParameters) =>
            clothesDetailsParameters.ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound, nameof(ClothesDetailNavigationParameters))).
            ResultValueOk(parameters => parameters).
            Map(result => result.Value);

        /// <summary>
        /// Получить модели изображений
        /// </summary>
        private static async Task<IReadOnlyCollection<ClothesDetailImageViewModelItem>> GetClothesImages(IClothesRestService clothesRestService, int clothesId) =>
            await clothesRestService.GetImages(clothesId).
            ResultCollectionOkTaskAsync(clothes => clothes.OrderBy(clothesItem => clothesItem.IsMain).
                                                   ThenBy(clothesItem => clothesItem.Id)).
            ResultCollectionOkTaskAsync(clothes => 
                clothes.Select(clothesItem => new ClothesDetailImageViewModelItem(clothesItem.Image))).
             WhereContinueTaskAsync(result => result.OkStatus,
                                    result => result.Value,
                                    result => new List<ClothesDetailImageViewModelItem>());
    }
}