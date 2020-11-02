using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntity;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes
{
    public interface IClothesInformationEntityConverter : 
        IEntityConverter<int, IClothesFullDomain, IClothesEntity, ClothesInformationEntity>
    { }
}