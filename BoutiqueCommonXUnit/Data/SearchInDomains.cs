using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Поиск в моделях
    /// </summary>
    public static class SearchInDomains
    {
        /// <summary>
        /// Найти модель
        /// </summary>
        public static ITestDomain FirstDomain(IEnumerable<ITestDomain> domains, TestEnum id) =>
            domains.First(domain => domain.Id == id);
    }
}