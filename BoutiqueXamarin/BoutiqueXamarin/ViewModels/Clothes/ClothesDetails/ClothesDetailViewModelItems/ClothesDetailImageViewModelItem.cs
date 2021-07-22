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
        public ClothesDetailImageViewModelItem(ImageSource image)
        {
            Image = image;
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource Image { get; }
    }
}