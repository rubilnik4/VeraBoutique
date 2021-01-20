using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Выбор типа одежды. Вложенная модель
    /// </summary>
    public class ChoiceViewModelItem
    {
        public ChoiceViewModelItem(IGenderDomain gender)
        {
            Gender = gender;
        }

        /// <summary>
        /// Пол
        /// </summary>
        public IGenderDomain Gender { get; }
    }
}