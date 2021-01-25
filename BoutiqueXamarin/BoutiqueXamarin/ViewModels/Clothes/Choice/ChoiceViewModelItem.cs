using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Тип одежды
    /// </summary>
    public class ChoiceViewModelItem
    {
        public ChoiceViewModelItem(IGenderDomain gender)
        {
            _gender = gender;
        }

        /// <summary>
        /// Пол
        /// </summary>
        private readonly IGenderDomain _gender;

        /// <summary>
        /// Наименование типа пола
        /// </summary>
        public string Name => _gender.Name;
    }
}