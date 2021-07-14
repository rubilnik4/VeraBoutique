using Xamarin.Forms;

namespace BoutiqueXamarin.Controls
{
    /// <summary>
    /// RadioButton
    /// </summary>
    public class StackRadioButton: StackLayout
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
            BindableProperty.Create(nameof(Text), typeof(string), typeof(StackLayout));

        /// <summary>
        /// Выбор
        /// </summary>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        /// Выбор
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(StackLayout));

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Текст
        /// </summary>
        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(nameof(GroupName), typeof(string), typeof(StackLayout));
    }
}