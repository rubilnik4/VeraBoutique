using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Пол. Сущность базы данных
    /// </summary>
    public class ClothesEntity : ClothesType, IClothesTypeEntity
    {
        public ClothesEntity(string name, IEnumerable<GenderType> genders)
            : base(name, genders)
        { }
    }
}