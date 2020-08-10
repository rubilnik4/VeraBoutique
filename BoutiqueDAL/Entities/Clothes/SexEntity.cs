using BoutiqueCommon.Models.Enums;
using BoutiqueDAL.Entities.Base;

namespace BoutiqueDAL.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class SexEntity: BaseEntity<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id { get; protected set; }

        /// <summary>
        /// Тип
        /// </summary>
        public virtual SexType Type { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; set; }
    }
}