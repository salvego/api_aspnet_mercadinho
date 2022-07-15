using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class CartItemsMap : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            //Tabela
            builder.ToTable("CartItems");

            builder.HasKey(x => new {x.CartId, x.ProductId});
            
            builder.Property(x => x.Quantity)
                .IsRequired() // NOT NULL
                .HasColumnName("Quantity") //NOME DA COLUNA
                .HasColumnType("float"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.PriceUnit)
                .IsRequired() // NOT NULL
                .HasColumnName("PriceUnit") //NOME DA COLUNA
                .HasColumnType("float"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.UnMed)
                .IsRequired() // NOT NULL
                .HasColumnName("UnMed") //NOME DA COLUNA
                .HasColumnType("varchar")
                .HasMaxLength(5); //TIPO DE DADO DA COLUNA

        }
    }
}