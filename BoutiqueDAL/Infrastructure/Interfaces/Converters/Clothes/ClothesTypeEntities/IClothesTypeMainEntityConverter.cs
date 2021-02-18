using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeMainEntityConverter : IEntityConverter<string, IClothesTypeMainDomain, ClothesTypeEntity>
    { }
}