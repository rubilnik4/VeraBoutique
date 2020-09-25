using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public class BoutiqueEntityDatabase : EntityDatabase, IBoutiqueDatabase
    {
        public BoutiqueEntityDatabase(DbContextOptions options)
            : base(options)
        { }

        /// <summary>
        /// Таблица пола базы данных EntityFramework
        /// </summary>
        public DbSet<GenderEntity> Genders { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных вида одежды EntityFramework
        /// </summary>
        public DbSet<ClothesTypeEntity> ClothesTypes { get; set; } = null!;

        /// <summary>
        /// Таблица, связующая сущность пола и вид одежды
        /// </summary>
        public DbSet<ClothesTypeGenderEntity> ClothesTypeGenders { get; set; } = null!;

        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IGenderTable GendersTable => new GenderTable(Genders);

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        public IClothesTypeTable ClotheTypeTable => new ClothesTypeTable(ClothesTypes);

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public async Task UpdateSchema(UserManager<IdentityUser> userManager, IResultCollection<BoutiqueUser> defaultUsers)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();
            await IdentityInitialize.Initialize(this, userManager, defaultUsers);
        }

        /// <summary>
        /// Записать параметры конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GenderType>();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeGenderConfiguration());
            modelBuilder.HasPostgresEnum<GenderType>();

            InitializeEntityData(modelBuilder);
        }

        /// <summary>
        /// Инициализация данными таблиц
        /// </summary>
        private static void InitializeEntityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderEntity>().HasData(GenderInitialize.GenderData);
            modelBuilder.Entity<ClothesTypeEntity>().HasData(ClothesTypeInitialize.ClothesTypeData);
            modelBuilder.Entity<ClothesTypeGenderEntity>().HasData(ClothesTypeGenderInitialize.ClothesTypeGenderData);
        }
    }
}