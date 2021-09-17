using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.ViewModels.Base;

namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems
{
    /// <summary>
    /// Детальное описание одежды
    /// </summary>
    public class ClothesDetailDescriptionViewModel: BaseViewModel
    {
        public ClothesDetailDescriptionViewModel (IClothesDetailDomain clothesDetail, SizeType defaultSizeType)
        {
            _clothesDetail = clothesDetail;
            _defaultSizeType = defaultSizeType;
        }

        /// <summary>
        /// Одежда. Уточненная информация. Доменная модель
        /// </summary>
        private readonly IClothesDetailDomain _clothesDetail;

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        private readonly SizeType _defaultSizeType;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name =>
            _clothesDetail.Name;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description =>
            _clothesDetail.Description;

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price =>
            _clothesDetail.Price;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<ISizeGroupDefaultDomain> Sizes =>
           _clothesDetail.SizeGroups.
           Select(sizeGroup => new SizeGroupDefaultDomain(sizeGroup, _defaultSizeType)).
           ToList();

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<IColorDomain> Colors =>
            _clothesDetail.Colors;
    }
}