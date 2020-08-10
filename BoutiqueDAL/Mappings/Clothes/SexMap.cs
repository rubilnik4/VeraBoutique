using BoutiqueCommon.Models.Enums;
using BoutiqueDAL.Entities.Clothes;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace BoutiqueDAL.Mappings.Clothes
{
    /// <summary>
    /// Пол. Схема базы данных
    /// </summary>
    public class SexMap : ClassMap<SexEntity>
    {
        public SexMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Type).CustomType<SexType>().Not.Nullable();
            Map(x => x.Name).Not.Nullable();
        }
    }
}