using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes
{
    public interface IClothesInformationEntityConverter : 
        IEntityConverter<int, IClothesInformationDomain, IClothesInformationEntity, ClothesInformationEntity>
    { }
}