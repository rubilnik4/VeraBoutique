using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesInformationEntity : ClothesInformation, IClothesInformationEntity
    {
        public ClothesInformationEntity(int id, string name, string description, 
                                        decimal price, byte[]? image)
            : base(id, name, description, price, image)
        { }
    }
}