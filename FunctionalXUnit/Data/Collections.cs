using System.Collections.Generic;
using System.Linq;

namespace FunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые примеры коллекций
    /// </summary>
    public static class Collections
    {
        public static IReadOnlyCollection<int> GetRangeNumber() =>
            Enumerable.Range(0, 3).ToList().AsReadOnly();
    }
}