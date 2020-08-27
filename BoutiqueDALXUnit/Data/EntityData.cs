using System.Collections.Generic;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Clothes;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые данные сущностей
    /// </summary>
    public static class EntityData
    {
        /// <summary>
        /// Получить типы полов
        /// </summary>
        public static List<GenderEntity> GetGenders() =>
            new List<GenderEntity>()
            {
                new GenderEntity( GenderType.Male,"Мужик" ),
                new GenderEntity(GenderType.Female,"Тетя"),
            };
    }
}