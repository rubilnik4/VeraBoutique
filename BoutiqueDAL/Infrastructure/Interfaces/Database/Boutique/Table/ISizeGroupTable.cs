using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных группы размеров одежды
    /// </summary>
    public interface ISizeGroupTable : IDatabaseTable<(ClothesSizeType, int), SizeGroupEntity>
    { }
}