using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesTransfer : 
        ClothesBase<GenderTransfer, ClothesTypeShortTransfer, ColorTransfer, SizeGroupTransfer, SizeTransfer>,
        IClothesTransfer
    {
        public ClothesTransfer(IClothesShortBase clothes,
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort,
                               IEnumerable<ColorTransfer> colors, IEnumerable<SizeGroupTransfer> sizeGroups)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                  gender, clothesTypeShort, colors.ToList(), sizeGroups.ToList())
        { }

        [JsonConstructor]
        public ClothesTransfer(int id, string name, string description, decimal price, byte[] image,
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort,
                               IReadOnlyCollection<ColorTransfer> colors, IReadOnlyCollection<SizeGroupTransfer> sizeGroups)
            : base(id, name, description, price, image, gender, clothesTypeShort, colors, sizeGroups)
        { }
    }
}