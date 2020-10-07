using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public interface ISizeGroupEntity : ISizeGroup<SizeEntity>, IEntityModel<(ClothesSizeType, int)>
    {
        /// <summary>
        /// Связующая сущность группы размера одежды
        /// </summary>
        IReadOnlyCollection<SizeGroupCompositeEntity> SizeGroupCompositeEntities { get; }
    }
}