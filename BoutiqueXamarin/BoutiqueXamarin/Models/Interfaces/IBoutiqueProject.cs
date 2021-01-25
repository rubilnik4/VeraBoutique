﻿using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueXamarin.Models.Interfaces
{
    /// <summary>
    /// Модель магазина одежды
    /// </summary>
    public interface IBoutiqueProject
    {
        /// <summary>
        /// Типы пола
        /// </summary>
        IReadOnlyCollection<IGenderDomain> Genders { get; }
    }
}