using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public interface IClothesSizeGroupEntity : ISizeGroup, IEntityModel<string>
    {

    }
}