using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueXamarin.Controls;
using BoutiqueXamarin.Controls.Enums;
using BoutiqueXamarin.Controls.Extensions;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Sync;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Essentials;
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

            this.WhenAnyValue(x => x.ValidationType).
                 Select(validationType => validationType.ToInputType()).
                 BindTo(this, x => x.MainEntry.Keyboard);

            this.WhenAnyValue(x => x.MainEntry.Text).
                 Select(text => EntryValidation.IsValid(ValidationType, text)).
                 BindTo(this, x => x.IsValid);
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