using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities
{
    public interface ISizeGroupShortEntityConverter :
        IEntityConverter<int, ISizeGroupShortDomain, SizeGroupShortEntity>
    { }
}