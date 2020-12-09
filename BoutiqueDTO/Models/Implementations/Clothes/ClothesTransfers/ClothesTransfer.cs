﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesTransfer : ClothesShortTransfer, IClothesTransfer
    {
        public ClothesTransfer()
        { }

        public ClothesTransfer(IClothesShortBase clothes,
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort,
                               IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizeGroups)
            :this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                  gender, clothesTypeShort, colors, sizeGroups)
        { }

        public ClothesTransfer(int id, string name, string description, decimal price, byte[]? image, 
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort, 
                               IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizeGroups)
            : base(id, name, description, price, image, gender.GenderType, clothesTypeShort.Name)
        {
            Gender = gender;
            ClothesTypeShort = clothesTypeShort;
            Colors = colors.ToList();
            SizeGroups = sizeGroups.ToList();
        }

        /// <summary>
        /// Пол одежды
        /// </summary>
        [Required]
        public GenderTransfer Gender { get; set; } = null!;

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public ClothesTypeShortTransfer ClothesTypeShort { get; set; } = null!;

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<ColorClothesTransfer> Colors { get; set; } = null!;

        /// <summary>
        /// Размеры
        /// </summary>
        [Required]
        public IReadOnlyCollection<SizeGroupTransfer> SizeGroups { get; set; } = null!;
    }
}