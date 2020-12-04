using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data
{
    /// <summary>
    /// Тестовые трансферные модели
    /// </summary>
    public static class TestTransferData
    {
        /// <summary>
        /// Тестовые трансферные модели
        /// </summary>
        public static IReadOnlyCollection<TestTransfer> TestTransfers =>
            TestData.TestDomains.
            Select(test =>
                new TestTransfer(test, test.TestIncludes.
                                       Select(testInclude => new TestIncludeTransfer(testInclude)))).
            ToList();
    }
}