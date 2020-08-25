using System;
using BoutiqueCommon.Models.Enums;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Base;

namespace BoutiqueDAL.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity: BaseEntity<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id { get; protected set; }

        /// <summary>
        /// Тип
        /// </summary>
        public GenderType GenderType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = String.Empty;
    }
}