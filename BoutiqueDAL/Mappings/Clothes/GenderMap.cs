using BoutiqueCommon.Models.Enums;
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
            Id(gender => gender.Id).GeneratedBy.Identity();
            Map(gender => gender.GenderType).CustomType<GenderType>().Not.Nullable();
            Map(gender => gender.Name).Not.Nullable();
        }
    }
}