using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Models.Interfaces;

namespace BoutiqueXamarin.Models.Implementation
{
    /// <summary>
    /// Модель магазина одежды
    /// </summary>
    public class BoutiqueProject: IBoutiqueProject
    {
        public BoutiqueProject()
        {
            Genders = new List<IGenderDomain>()
            {
                new GenderDomain(new GenderDomain(GenderType.Male, "Хуй"))
            };
        }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<IGenderDomain> Genders { get; } //= new List<IGenderDomain>();
    }
}