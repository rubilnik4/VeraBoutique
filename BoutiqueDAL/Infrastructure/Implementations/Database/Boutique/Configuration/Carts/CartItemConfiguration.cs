using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Carts
{
    /// <summary>
    /// Позиции корзины. Схема базы данных
    /// </summary>
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Price).IsRequired();

            builder.HasOne(t => t.CartEntity)
                   .WithMany(s => s!.CartItems)
                   .HasForeignKey(sc => sc.CartId)
                   .IsRequired();
        }
    }
}