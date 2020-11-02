namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Информация
    /// </summary>
    public interface IClothes: IClothesShort
    {
        /// <summary>
        /// Описание
        /// </summary>
        string Description { get; }
    }
}