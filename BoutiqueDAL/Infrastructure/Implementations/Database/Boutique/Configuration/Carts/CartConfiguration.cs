using BoutiqueDAL.Models.Implementations.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Carts
{
    /// <summary>
    /// Позиции корзины. Схема базы данных
    /// </summary>
    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.HasKey(t => t.Id);
          //  builder.Property(t => t.Id).HasDefaultValueSql("NEWID()").IsRequired();
        }
    }
}