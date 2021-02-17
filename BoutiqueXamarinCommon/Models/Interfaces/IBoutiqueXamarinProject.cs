using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

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
        IReadOnlyCollection<IGenderDomain> Genders { get; }
    }
}