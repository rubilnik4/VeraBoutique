﻿using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommonXUnit.Data.Authorize
{
    /// <summary>
    /// Личная информация
    /// </summary>
    public static class PersonalData
    {
        /// <summary>
        /// Получить личные данные
        /// </summary>
        public static IReadOnlyCollection<IPersonalDomain> PersonalDomains =>
            new List<IPersonalDomain>
            {
                new PersonalDomain("Name", "Surname", "Address", "+79224725787"),
                new PersonalDomain("Name", "Surname", "Address", "+79224725787"),
            };
    }
}