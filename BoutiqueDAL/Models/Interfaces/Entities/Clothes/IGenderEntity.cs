using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public interface IGenderEntity: IEntityModel<GenderType>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        GenderType Id { get; }

        /// <summary>
        /// Тип
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}