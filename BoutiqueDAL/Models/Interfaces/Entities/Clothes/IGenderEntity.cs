using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Пол. Сущность базы данных
    /// </summary>
    public interface IGenderEntity: IGenderBase, IEntityModel<GenderType>
    {
        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

        /// <summary>
        /// Связующие сущности пола и одежды
        /// </summary>
        IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}