using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    public class ClothesInformationDomain: ClothesInformation, IClothesInformationDomain
    {
        public ClothesInformationDomain(int id, string name, string description, 
                                           string color, IReadOnlyCollection<int> sizes,
                                           decimal price, byte[]? image)
            : base(id, name, description, color, sizes, price, image)
        { }
    }
}