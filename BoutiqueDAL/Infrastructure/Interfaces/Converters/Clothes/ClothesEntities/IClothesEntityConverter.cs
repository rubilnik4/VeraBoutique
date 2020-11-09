using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities
{
    public interface IClothesEntityConverter : 
        IEntityConverter<int, IClothesDomain, IClothesEntity, ClothesEntity>
    { }
}