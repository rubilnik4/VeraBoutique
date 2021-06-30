using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    public class ClothesDetailTransfer :
        ClothesDetailBase<ColorTransfer, SizeGroupMainTransfer, SizeTransfer>,
        IClothesDetailTransfer
    {
        public ClothesDetailTransfer(IClothesBase clothes, 
                                     IEnumerable<ColorTransfer> colors, IEnumerable<SizeGroupMainTransfer> sizeGroups)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price,
                   clothes.GenderType, clothes.ClothesTypeName, colors.ToList(), sizeGroups.ToList())
        { }

        [JsonConstructor]
        public ClothesDetailTransfer(int id, string name, string description, decimal price,
                                     GenderType genderType, string clothesTypeName,
                                     IReadOnlyCollection<ColorTransfer> colors, IReadOnlyCollection<SizeGroupMainTransfer> sizeGroups)
            : base(id, name, description, price, genderType, clothesTypeName, colors, sizeGroups)
        { }
    }
}