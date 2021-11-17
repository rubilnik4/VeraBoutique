using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Кнопка-область
    /// </summary>
    public class StackLayoutButton: StackLayout
    {
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
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(StackLayoutButton));

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
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(StackLayoutButton));

        /// <summary>
        /// Текст кнопки
        /// </summary>
        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        /// <summary>
        /// Текст кнопки
        /// </summary>
        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(StackLayoutButton));

        /// <summary>
        /// Стиль кнопки
        /// </summary>
        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        /// <summary>
        /// Стиль кнопки
        /// </summary>
        public static readonly BindableProperty ButtonStyleProperty =
            BindableProperty.Create(nameof(ButtonStyle), typeof(Style), typeof(StackLayoutButton));
    }
}