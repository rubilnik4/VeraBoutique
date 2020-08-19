﻿using BoutiqueCommon.Models.Enums;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Clothes;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace BoutiqueDAL.Mappings.Clothes
{
    /// <summary>
    /// Пол. Схема базы данных
    /// </summary>
    public class GenderMap : ClassMap<GenderEntity>
    {
        public GenderMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.GenderType).CustomType<GenderType>().Not.Nullable();
            Map(x => x.Name).Not.Nullable();
        }
    }
}