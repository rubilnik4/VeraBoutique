namespace BoutiqueXamarin.ViewModels.Base
{
    /// <summary>
    /// Модель с выбором
    /// </summary>
    public abstract class CheckViewModelItem: BaseViewModel
    {
        /// <summary>
        /// Выбор
        /// </summary>
        public bool IsChecked { get; set; }
    }
}