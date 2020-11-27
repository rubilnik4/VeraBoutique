using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные. Сущность базы данных
    /// </summary>
    public interface ISizeGroupShortEntity : ISizeGroup, IEntityModel<(ClothesSizeType, int)>
    { }
}