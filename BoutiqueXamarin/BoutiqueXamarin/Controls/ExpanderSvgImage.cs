using System;
using BoutiqueXamarin.Views.Controls;
using FFImageLoading.Svg.Forms;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using ImageSource = FFImageLoading.Work.ImageSource;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Элемент раскрывающегося списка
    /// </summary>
    public class ExpanderCheck: Expander
    {
        public ExpanderCheck()
        {
            Header = new ExpanderHeaderView();
        }

        /// <summary>
        /// Изображение нераскрытого списка
        /// </summary>
        public string HeaderText
        {
            get => (string)GetValue(NotExpandSourceProperty);
            set => SetValue(NotExpandSourceProperty, value);
        }

        /// <summary>
        /// Изображение нераскрытого списка
        /// </summary>
        public static readonly BindableProperty NotExpandSourceProperty =
            BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(ExpanderCheck));
    }
}