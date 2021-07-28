using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesMainTransfer :
        ClothesMainBase<ClothesImageTransfer, GenderTransfer, ClothesTypeTransfer, ColorTransfer, SizeGroupMainTransfer, SizeTransfer>,
        IClothesMainTransfer
    {
        public ClothesMainTransfer(IClothesBase clothes, IEnumerable<ClothesImageTransfer> images,
                                   GenderTransfer gender, ClothesTypeTransfer clothesType,
                                   IEnumerable<ColorTransfer> colors, IEnumerable<SizeGroupMainTransfer> sizeGroups)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, images.ToList(),
                   gender, clothesType, colors.ToList(), sizeGroups.ToList())
        { }

        [JsonConstructor]
        public ClothesMainTransfer(int id, string name, string description, decimal price,
                                   IReadOnlyCollection<ClothesImageTransfer> images,
                                   GenderTransfer gender, ClothesTypeTransfer clothesType,
                                   IReadOnlyCollection<ColorTransfer> colors, IReadOnlyCollection<SizeGroupMainTransfer> sizeGroups)
            : base(id, name, description, price, images, gender, clothesType, colors, sizeGroups)
        { }
    }
}