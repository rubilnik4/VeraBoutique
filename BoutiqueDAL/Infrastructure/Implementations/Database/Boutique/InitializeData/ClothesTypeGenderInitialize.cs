using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData
{
    /// <summary>
    /// Начальные данные таблицы, связующей пол и вид одежды
    /// </summary>
    public static class ClothesTypeGenderInitialize
    {
        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderData =>
            new List<ClothesTypeGenderEntity>()
            {
               new ClothesTypeGenderEntity(ClothesTypeInitialize.ClothesTypeData.FirstOrDefault().Id, GenderInitialize.Male.Id),
               new ClothesTypeGenderEntity(ClothesTypeInitialize.ClothesTypeData.FirstOrDefault().Id, GenderInitialize.Female.Id),
            }.AsReadOnly();
    }
}