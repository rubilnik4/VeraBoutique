using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных вида одежды
    /// </summary>
    public interface IClothesTypeTable : IDatabaseTable<string, ClothesTypeEntity>
    { }
}