using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public interface ISizeGroupMainEntityConverter : IEntityConverter<int, ISizeGroupMainDomain, SizeGroupEntity>
    { }
}