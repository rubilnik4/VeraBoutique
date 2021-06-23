using System;
using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    public class CheckBoxButton: Button
    {
        public CheckBoxButton()
        {
            this.Clicked += CheckBoxButton_Clicked;
        }

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
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxButton), default(bool));

        /// <summary>
        /// Текст при пустом условии
        /// </summary>
        public string TextUnchecked
        {
            get => (string)GetValue(TextUncheckedProperty);
            set => SetValue(TextUncheckedProperty, value);
        }

        /// <summary>
        /// Выбор. Свойство
        /// </summary>
        public static readonly BindableProperty TextUncheckedProperty =
            BindableProperty.Create(nameof(TextUnchecked), typeof(string), typeof(CheckBoxButton), default(string),
                                    propertyChanged: OnTextUncheckedPropertyChanged);

        /// <summary>
        /// Изменение текста
        /// </summary>
        private static void OnTextUncheckedPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var checkBoxButton = (CheckBoxButton)bindableObject;
            checkBoxButton.Text = (string)newValue;
        }


        /// <summary>
        /// Текст при пустом условии
        /// </summary>
        public string TextChecked
        {
            get => (string)GetValue(TextCheckedProperty);
            set => SetValue(TextCheckedProperty, value);
        }

        /// <summary>
        /// Выбор. Свойство
        /// </summary>
        public static readonly BindableProperty TextCheckedProperty =
            BindableProperty.Create(nameof(TextChecked), typeof(string), typeof(CheckBoxButton), default(string));

        /// <summary>
        /// Изменить выбор
        /// </summary>
        private void CheckBoxButton_Clicked(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            Text = IsChecked ? TextChecked : TextUnchecked;
        }
    }
}