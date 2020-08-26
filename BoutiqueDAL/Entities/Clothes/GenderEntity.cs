﻿using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Base;

namespace BoutiqueDAL.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity : BaseEntity<int>, IEqualEntity<GenderEntity>
    {
        public GenderEntity(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id { get; protected set; }

        /// <summary>
        /// Тип
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Идентичны ли сущности
        /// </summary>
        public bool EqualEntity(GenderEntity? entity) =>
            GenderType == entity?.GenderType &&
            Name == entity?.Name;
    }
}