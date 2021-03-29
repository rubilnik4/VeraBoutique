using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

namespace BoutiqueXamarinCommon.Models.Interfaces
{
    /// <summary>
    /// Общие данные клиентского приложения
    /// </summary>
    public interface IBoutiqueXamarinProject
    {
        /// <summary>
        /// Типы пола
        /// </summary>
        IReadOnlyCollection<IGenderCategoryDomain> GenderCategories { get; }
    }
}