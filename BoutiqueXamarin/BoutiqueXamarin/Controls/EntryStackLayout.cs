using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// Текстовое поле
    /// </summary>
    public class EntryStackLayout : StackLayout
    {
        /// <summary>
        /// Текст
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Текст
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(StackLayout), propertyChanged: OnTextPropertyChanged);

        /// <summary>
        /// Изменение текста
        /// </summary>
        private static void OnTextPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var entryStackLayout = (EntryStackLayout) bindableObject;
            entryStackLayout.HasError = false;
        }

        /// <summary>
        /// Цвет при выборе
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Цвет при выборе
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(StackLayout), SelectedColorDefault);

        /// <summary>
        /// Текст подписи
        /// </summary>
        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        /// <summary>
        /// Текст подписи
        /// </summary>
        public static readonly BindableProperty PlaceholderTextProperty =
            BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(StackLayout));

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(StackLayout), PlaceholderColorDefault);

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(StackLayout), ErrorColorDefault);

        /// <summary>
        /// Наличие ошибки
        /// </summary>
        public bool HasError
        {
            get => (bool)GetValue(HasErrorProperty);
            set => SetValue(HasErrorProperty, value);
        }

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public static readonly BindableProperty HasErrorProperty =
            BindableProperty.Create(nameof(HasError), typeof(bool), typeof(StackLayout));

        /// <summary>
        /// Является ли паролем
        /// </summary>
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        /// <summary>
        /// Является ли паролем
        /// </summary>
        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(StackLayout));

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        /// <summary>
        /// Цвет подписи
        /// </summary>
        public static readonly BindableProperty ErrorMessageProperty =
            BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(StackLayout));

        /// <summary>
        /// Цвет при выборе по умолчанию
        /// </summary>
        private static Color SelectedColorDefault => 
            new Color(255, 0, 0, 0);

        /// <summary>
        /// Цвет при выборе по умолчанию
        /// </summary>
        private static Color PlaceholderColorDefault =>
            new Color(255, 158, 158, 158);

        /// <summary>
        /// Цвет при выборе по умолчанию
        /// </summary>
        private static Color ErrorColorDefault =>
            new Color(255, 175, 0, 32);
    }
}