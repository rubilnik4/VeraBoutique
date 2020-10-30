namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public interface IClothesTypeDomain : IClothesTypeShortDomain
    {
        /// <summary>
        /// Тип пола. Доменная модель
        /// </summary>
        IGenderDomain GenderDomain { get; }

        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        ICategoryDomain CategoryDomain { get; }
    }
}