using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;

namespace BoutiqueXamarin.Models.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Параметры навигации к странице выбора одежды
    /// </summary>
    public class ChoiceNavigationOptions : BaseNavigationOptions
    {
        public ChoiceNavigationOptions(IReadOnlyCollection<IGenderCategoryDomain> genderCategories)
        {
            GenderCategories = genderCategories;
        }

        /// <summary>
        /// Данные типа пола с категорией
        /// </summary>
        public IReadOnlyCollection<IGenderCategoryDomain> GenderCategories { get; }
    }
}