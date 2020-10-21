using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public interface ISizeEntity : ISize, IEntityModel<(SizeType, string)>
    {
        /// <summary>
        /// Связующая сущность размера одежды
        /// </summary>
        IReadOnlyCollection<SizeGroupCompositeEntity> SizeGroupCompositeEntities { get; }
    }
}