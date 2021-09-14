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
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails
{
    public class ClothesDetailViewModel : NavigationLoginViewModel<ClothesDetailNavigationParameters, IClothesDetailNavigationService>
    {
        public ClothesDetailViewModel(IClothesRestService clothesRestService,
                                      IClothesDetailNavigationService clothesDetailNavigationService, 
                                      ILoginNavigationService loginNavigationService)
            : base(clothesDetailNavigationService, loginNavigationService)
        {
            _clothesDetailDescriptionViewModel = GetClothesDetailDescriptionViewModelObservable();
            _clothesDetailImageViewModelItems = GetClothesDetailImageViewModelsObservable(clothesRestService);
        }

        /// <summary>
        /// Детальное описание одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<ClothesDetailDescriptionViewModel> _clothesDetailDescriptionViewModel;

        /// <summary>
        /// Детальное описание одежды
        /// </summary>
        public ClothesDetailDescriptionViewModel ClothesDetailDescriptionViewModel =>
            _clothesDetailDescriptionViewModel.Value;

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
        /// Получить модели детальной одежды
        /// </summary>
        private ObservableAsPropertyHelper<ClothesDetailDescriptionViewModel> GetClothesDetailDescriptionViewModelObservable() =>
            this.WhenAnyValue(x => x.NavigationParameters).
                 WhereNotNull().
                 Select(parameters => new ClothesDetailDescriptionViewModel(parameters.ClothesDetail, parameters.DefaultSizeType)).
                 ToProperty(this, nameof(ClothesDetailDescriptionViewModel));

        /// <summary>
        /// Получить модели детальной одежды
        /// </summary>
        private ObservableAsPropertyHelper<IReadOnlyCollection<ClothesDetailImageViewModelItem>> GetClothesDetailImageViewModelsObservable(IClothesRestService clothesRestService) =>
            this.WhenAnyValue(x => x.NavigationParameters).
                 WhereNotNull().
                 SelectMany(parameters => Observable.FromAsync(() => GetClothesImages(clothesRestService, parameters.ClothesDetail.Id))).
                 ToProperty(this, nameof(ClothesDetailImageViewModelItems), scheduler: RxApp.MainThreadScheduler);

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