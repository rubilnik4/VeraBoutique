using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueXamarin.Controls;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoutiqueXamarin.Views.ControlViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryControl : EntryStackLayout
    {
        public EntryControl()
        {
            InitializeComponent();

            this.MainEntry.Events().Focused.
                 Merge(MainEntry.Events().Unfocused).
                 Select(eventArg => eventArg.IsFocused).
                 Subscribe(isFocused => UnderlineChange(isFocused, HasError));

            this.WhenAnyValue(x => x.HasError).
                 Subscribe(error => UnderlineChange(MainEntry.IsFocused, error));
        }

        /// <summary>
        /// Ширина подчеркивания при выделении
        /// </summary>
        private static int UnderlineWidthSelected => 2;

        /// <summary>
        /// Ширина подчеркивания при отсутствии выделения
        /// </summary>
        private static int UnderlineWidthUnselected => 1;

        /// <summary>
        /// Изменить подчеркивающую линию
        /// </summary>
        private void UnderlineChange(bool isFocused, bool hasError)
        {
            Underline.HeightRequest = isFocused ? UnderlineWidthSelected : UnderlineWidthUnselected;
            Underline.Color = GetUnderlineColor(isFocused, hasError);
        }

        /// <summary>
        /// Получить цвет линии
        /// </summary>
        private Color GetUnderlineColor(bool isFocused, bool hasError) =>
             (isFocused, hasError) switch
             {
                 (false, false) => PlaceholderColor,
                 (true, false) => SelectedColor,
                 (_, true) => ErrorColor,
             };
    }
}