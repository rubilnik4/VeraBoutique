using System;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Кнопка с иконкой
    /// </summary>
    public class ImageButtonGlyph: ImageButton
    {
        public ImageButtonGlyph()
        {
            FontImageSource = new FontImageSource();
            Source = FontImageSource;
        }

        /// <summary>
        /// Иконка в виде шрифта
        /// </summary>
        public FontImageSource FontImageSource { get; }

        /// <summary>
        /// Иконка
        /// </summary>
        public string Glyph 
        {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }

        /// <summary>
        /// Иконка. Свойство
        /// </summary>
        public static readonly BindableProperty GlyphProperty = 
            BindableProperty.Create(nameof(Glyph), typeof(string), typeof(ImageButtonGlyph), default(string), 
                                    propertyChanged: OnGlyphPropertyChanged);

        /// <summary>
        /// Изменение иконки
        /// </summary>
        private static void OnGlyphPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var imageButtonGlyph =(ImageButtonGlyph)bindableObject;
            imageButtonGlyph.FontImageSource.Glyph = (string)newValue;
        }

        /// <summary>
        /// Семейство шрифтов
        /// </summary>
        public string GlyphFontFamily
        {
            get => (string)GetValue(GlyphFontFamilyProperty);
            set => SetValue(GlyphFontFamilyProperty, value);
        }

        /// <summary>
        /// Семейство шрифтов. Свойство
        /// </summary>
        public static readonly BindableProperty GlyphFontFamilyProperty =
            BindableProperty.Create(nameof(GlyphFontFamily), typeof(string), typeof(ImageButtonGlyph), default(string),
                                    propertyChanged: OnGlyphFontFamilyPropertyChanged);

        /// <summary>
        /// Изменение семейства шрифтов
        /// </summary>
        private static void OnGlyphFontFamilyPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var imageButtonGlyph = (ImageButtonGlyph)bindableObject;
            imageButtonGlyph.FontImageSource.FontFamily = (string)newValue;
        }

        /// <summary>
        /// Цвет иконки
        /// </summary>
        public Color GlyphColor
        {
            get => (Color)GetValue(GlyphColorProperty);
            set => SetValue(GlyphColorProperty, value);
        }

        /// <summary>
        /// Цвет иконки. Свойство
        /// </summary>
        public static readonly BindableProperty GlyphColorProperty =
            BindableProperty.Create(nameof(GlyphColor), typeof(Color), typeof(ImageButtonGlyph), default(Color),
                                    propertyChanged: OnGlyphColorPropertyChanged);

        /// <summary>
        /// Изменение цвета
        /// </summary>
        private static void OnGlyphColorPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var imageButtonGlyph = (ImageButtonGlyph)bindableObject;
            imageButtonGlyph.FontImageSource.Color = (Color)newValue;
        }

        /// <summary>
        /// Размер иконки
        /// </summary>
        public double GlyphSize
        {
            get => (double)GetValue(GlyphSizeProperty);
            set => SetValue(GlyphSizeProperty, value);
        }

        /// <summary>
        /// Размер иконки. Свойство
        /// </summary>
        public static readonly BindableProperty GlyphSizeProperty =
            BindableProperty.Create(nameof(GlyphSize), typeof(double), typeof(ImageButtonGlyph), default(double),
                                    propertyChanged: OnGlyphSizePropertyChanged);

        /// <summary>
        /// Изменение размера иконки
        /// </summary>
        private static void OnGlyphSizePropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var imageButtonGlyph = (ImageButtonGlyph)bindableObject;
            imageButtonGlyph.FontImageSource.Size = (double)newValue;
        }
    }
}