using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public interface ISizeEntity : ISize, IEntityModel<(SizeType, int)>
    {
        ///// <summary>
        ///// Идентификатор связующей сущности категории одежды
        ///// </summary>
        //public (ClothesSizeType, int)? SizeGroupEntityId { get; }

        ///// <summary>
        ///// Связующая сущность категории одежды
        ///// </summary>
        //public SizeGroupEntity? SizeGroupEntity { get; }
    }
}