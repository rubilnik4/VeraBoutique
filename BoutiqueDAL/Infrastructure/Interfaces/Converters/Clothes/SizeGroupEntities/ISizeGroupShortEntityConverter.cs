using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities
{
    public interface ISizeGroupShortEntityConverter :
        IEntityConverter<(ClothesSizeType, int), ISizeGroupShortDomain, ISizeGroupShortEntity, SizeGroupShortEntity>
    { }
}