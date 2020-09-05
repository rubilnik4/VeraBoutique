using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity : IGenderEntity
    {
        public GenderEntity(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public GenderType Id => GenderType;

        /// <summary>
        /// Тип
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        ///// <summary>
        ///// Идентичны ли сущности
        ///// </summary>
        //public bool EqualEntity(GenderEntity? entity) =>
        //    GenderType == entity?.GenderType &&
        //    Name == entity?.Name;
    }
}