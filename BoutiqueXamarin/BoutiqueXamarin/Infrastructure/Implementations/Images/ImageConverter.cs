using System.IO;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Images
{
    /// <summary>
    /// Преобразование картинок
    /// </summary>
    public static class ImageConverter
    {
        /// <summary>
        /// Преобразовать байтовый массив изображения в поток
        /// </summary>
        public static ImageSource ToImageSource(byte[] image) =>
            ImageSource.FromStream(() => new MemoryStream(image));
    }
}