using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
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
            _images = Observable.Return(ImageSource.FromFile("empty_image.png")).
                                Select(image => (IReadOnlyCollection<ImageSource>)new List<ImageSource> { image }).
                                ToProperty(this, nameof(Images));

            _clothesDetail = this.WhenAnyValue(x => x.NavigationParameters).
                                  Where(parameters => parameters != null).
                                  Select(GetClothes).
                                  ToProperty(this, nameof(ClothesDetail));

            _images = this.WhenAnyValue(x => x.ClothesDetail).
                           Where(clothes => clothes != null).
                           SelectMany(clothes => GetImageSource(clothesRestService, clothes.Id)).
                           ToProperty(this, nameof(Images));


        }

        /// <summary>
        /// Информация об одежде
        /// </summary>
        private readonly ObservableAsPropertyHelper<IClothesDetailDomain> _clothesDetail;

        /// <summary>
        /// Информация об одежде
        /// </summary>
        public IClothesDetailDomain ClothesDetail =>
            _clothesDetail.Value;

        /// <summary>
        /// Изображения
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ImageSource>> _images;

        /// <summary>
        /// Изображения
        /// </summary>
        public IReadOnlyCollection<ImageSource> Images =>
            _images.Value;

        /// <summary>
        /// Изображения
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ImageSource>> _images;

        /// <summary>
        /// Изображения
        /// </summary>
        public IReadOnlyCollection<ImageSource> Images =>
            _images.Value;

        /// <summary>
        /// Получить информацию об одежде
        /// </summary>
        private static IClothesDetailDomain GetClothes(ClothesDetailNavigationParameters? clothesDetailsParameters) =>
            clothesDetailsParameters.ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound, nameof(ClothesDetailNavigationParameters))).
            ResultValueOk(parameters => parameters.ClothesDetail).
            Map(result => result.Value);

        ///// <summary>
        ///// Получить изображения
        ///// </summary>param>
        ///// <returns></returns>
        //private static IReadOnlyCollection<ImageSource> GetImages(IClothesMainDomain clothes) =>
        //      ImageSource.FromStream(() => new MemoryStream(clothes.Image)).
        //      Map(image => (IReadOnlyCollection<ImageSource>)new List<ImageSource> { image });

        /// <summary>
        /// Преобразовать изображение в поток
        /// </summary>
        private static async Task<IReadOnlyCollection<ImageSource>> GetImageSource(IClothesRestService clothesRestService, int clothesId) =>
            await clothesRestService.GetImage(clothesId).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   _ => new byte[0]).
            MapTaskAsync(bytes => ImageSource.FromStream(() => new MemoryStream(bytes))).
            MapTaskAsync(imageSource => new List<ImageSource> { imageSource });
    }
}