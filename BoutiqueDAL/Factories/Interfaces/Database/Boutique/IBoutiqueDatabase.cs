using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Base;

namespace BoutiqueDAL.Factories.Interfaces.Database.Boutique
{
    /// <summary>
    /// База данных магазина
    /// </summary>
    public interface IBoutiqueDatabase : IDatabase
    {
        IDatabaseTable<GenderEntity> Genders { get; }
    }
}