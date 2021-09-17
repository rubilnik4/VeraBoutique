using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using BoutiqueXamarin.Controls.Enums;
using BoutiqueXamarin.Controls.Extensions;
using ReactiveUI;
using Xamarin.Forms;
using static System.Char;

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
        /// Корректность поля
        /// </summary>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        /// <summary>
        /// Корректность поля
        /// </summary>
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(StackLayout));

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
        /// Только для чтения
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        /// Только для чтения
        /// </summary>
        public static readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(StackLayout));

        /// <summary>
        /// Тип проверки
        /// </summary>
        public EntryValidationType ValidationType
        {
            get => (EntryValidationType)GetValue(ValidationTypeProperty);
            set => SetValue(ValidationTypeProperty, value);
        }

        /// <summary>
        /// Тип проверки
        /// </summary>
        public static readonly BindableProperty ValidationTypeProperty =
            BindableProperty.Create(nameof(ValidationType), typeof(EntryValidationType), typeof(StackLayout), EntryValidationType.Default);

        /// <summary>
        /// Максимальная длина
        /// </summary>
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Максимальная длина
        /// </summary>
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(StackLayout), Int32.MaxValue);

        /// <summary>
        /// Маска поля
        /// </summary>
        public string Mask
        {
            get => (string)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        /// <summary>
        /// Маска поля
        /// </summary>
        public static readonly BindableProperty MaskProperty =
            BindableProperty.Create(nameof(Mask), typeof(string), typeof(StackLayout));

        /// <summary>
        /// Символ маски
        /// </summary>
        public char MaskChar
        {
            get => (char)GetValue(MaskCharProperty);
            set => SetValue(MaskCharProperty, value);
        }

        /// <summary>
        /// Маска поля
        /// </summary>
        public static readonly BindableProperty MaskCharProperty =
            BindableProperty.Create(nameof(MaskChar), typeof(char), typeof(StackLayout), MinValue);

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