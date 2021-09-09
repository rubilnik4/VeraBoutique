using System;
using System.Reactive;
using System.Reactive.Linq;
using FFImageLoading.Svg.Forms;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Крыжик с картинкой
    /// </summary>
    public class CheckBoxSvgButton : SvgCachedImage
    {
        public CheckBoxSvgButton()
        {
            var gestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(gestureRecognizer);
            Tapped = Observable.FromEventPattern(gestureRecognizer, nameof(gestureRecognizer.Tapped));

            this.Tapped.Subscribe(_ => IsChecked = !IsChecked);
            this.WhenAnyValue(x => x.IsChecked).
                 Subscribe(hasChecked => Source = hasChecked ? CheckSource : UncheckSource);
        }

        /// <summary>
        /// Событие нажатия
        /// </summary>
        public IObservable<EventPattern<object>> Tapped { get; }

        /// <summary>
        /// Выбор
        /// </summary>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// Выбор. Свойство
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxSvgButton), default(bool));

        /// <summary>
        /// Основное изображение
        /// </summary>
        public ImageSource UncheckSource
        {
            get => (ImageSource)GetValue(UncheckSourceProperty);
            set => SetValue(UncheckSourceProperty, value);
        }

        /// <summary>
        /// Основное изображение
        /// </summary>
        public static readonly BindableProperty UncheckSourceProperty =
            BindableProperty.Create(nameof(UncheckSource), typeof(ImageSource), typeof(CheckBoxSvgButton));

        /// <summary>
        /// Дополнительное изображение
        /// </summary>
        public ImageSource CheckSource
        {
            get => (ImageSource)GetValue(CheckSourceProperty);
            set => SetValue(CheckSourceProperty, value);
        }

        /// <summary>
        /// Дополнительное изображение
        /// </summary>
        public static readonly BindableProperty CheckSourceProperty =
            BindableProperty.Create(nameof(CheckSource), typeof(ImageSource), typeof(CheckBoxSvgButton));
    }
}