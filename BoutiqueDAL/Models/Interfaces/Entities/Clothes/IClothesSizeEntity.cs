using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public interface IClothesSizeEntity : ISize, IEntityModel<string>
    {

    }
}