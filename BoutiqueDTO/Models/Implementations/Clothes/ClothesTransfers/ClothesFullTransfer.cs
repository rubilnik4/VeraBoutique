using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
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
    public class ClothesFullTransfer : 
        ClothesFullBase<GenderTransfer, ClothesTypeTransfer, ColorTransfer, SizeGroupFullTransfer, SizeTransfer>,
        IClothesFullTransfer
    {
        public ClothesFullTransfer(IClothesBase clothes,
                               GenderTransfer gender, ClothesTypeTransfer clothesType,
                               IEnumerable<ColorTransfer> colors, IEnumerable<SizeGroupFullTransfer> sizeGroups)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                  gender, clothesType, colors.ToList(), sizeGroups.ToList())
        { }

        [JsonConstructor]
        public ClothesFullTransfer(int id, string name, string description, decimal price, byte[] image,
                               GenderTransfer gender, ClothesTypeTransfer clothesType,
                               IReadOnlyCollection<ColorTransfer> colors, IReadOnlyCollection<SizeGroupFullTransfer> sizeGroups)
            : base(id, name, description, price, image, gender, clothesType, colors, sizeGroups)
        { }
    }
}