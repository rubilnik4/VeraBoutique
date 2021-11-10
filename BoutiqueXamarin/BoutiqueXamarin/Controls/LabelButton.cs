using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Кнопка-надпись
    /// </summary>
    public class LabelButton: Label
    {
        public LabelButton()
        {
            var gestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(gestureRecognizer);
            Tapped = Observable.FromEventPattern(gestureRecognizer, nameof(gestureRecognizer.Tapped));

            this.Tapped.
                 Where(_ => Command?.CanExecute(CommandParameter) == true).
                 Subscribe(_ => Command?.Execute(CommandParameter));
        }

        /// <summary>
        /// Событие нажатия
        /// </summary>
        public IObservable<EventPattern<object>> Tapped { get; }

        /// <summary>
        /// Команда
        /// </summary>
        public ICommand? Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Команда
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LabelButton));

        /// <summary>
        /// Команда. Параметр
        /// </summary>
        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Команда. Параметр
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LabelButton));
    }
}