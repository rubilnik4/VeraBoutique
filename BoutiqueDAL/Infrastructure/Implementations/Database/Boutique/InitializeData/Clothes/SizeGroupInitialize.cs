using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы группы размеров одежды
    /// </summary>
    public static class SizeGroupInitialize
    {
        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeGroupData =>
            SizeInitialize.SizeData.
            Select(size => (size.ClothesSizeTypeId ?? throw new ArgumentNullException(nameof(size.ClothesSizeTypeId)),
                            size.SizeNormalizeId ?? throw new ArgumentNullException(nameof(size.SizeNormalizeId)))).
            Select(sizeId => new SizeGroupEntity(sizeId.Item1, sizeId.Item2)).
            Distinct().
            ToList().AsReadOnly();
    }
}