using System;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueEF.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public virtual GenderType GenderType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; set; } = String.Empty;
    }
}