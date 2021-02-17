using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueXamarinCommon.Models.Interfaces;

namespace BoutiqueXamarinCommon.Models.Implementation
{
    /// <summary>
    /// Общие данные клиентского приложения
    /// </summary>
    public class BoutiqueXamarinProject : IBoutiqueXamarinProject
    {
        public BoutiqueXamarinProject(IEnumerable<IGenderDomain> genders)
        {
            Genders = genders.ToList().AsReadOnly();
        }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<IGenderDomain> Genders { get; }
    }
}