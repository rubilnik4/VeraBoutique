namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Основная информация. Доменная модель
    /// </summary>
    public interface IClothesTypeShortDomain : IClothesTypeDomain
    {
        /// <summary>
        /// Тип пола. Доменная модель
        /// </summary>
        IGenderDomain Gender { get; }
    }
}