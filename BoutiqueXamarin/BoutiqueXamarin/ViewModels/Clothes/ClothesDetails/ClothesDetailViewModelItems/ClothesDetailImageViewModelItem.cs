using BoutiqueXamarin.Infrastructure.Implementations.Images;
using BoutiqueXamarin.ViewModels.Base;
using Xamarin.Forms;


namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems
{
    /// <summary>
    /// Подробная информация об одежде. Изображения
    /// </summary>
    public class ClothesDetailImageViewModelItem : BaseViewModel
    {
        public ClothesDetailImageViewModelItem(byte[] imageByte)
        {
              _imageByte = imageByte;
        }

        /// <summary>
        /// Изображение. Массив
        /// </summary>
        private readonly byte[] _imageByte;

        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource Image => ImageConverter.ToImageSource(_imageByte);
    }
}