using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public interface ISizeGroupEntity : ISizeGroupShortBase, IEntityModel<(ClothesSizeType, int)>
    {
        /// <summary>
        /// Связующая сущность группы размера одежды
        /// </summary>
        IReadOnlyCollection<SizeGroupCompositeEntity>? SizeGroupComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и размера
        /// </summary>
        IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}