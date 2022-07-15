using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class OrderItemsMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //Tabela
            builder.ToTable("OrderItem");

            builder.HasKey(x => new {x.OrderId, x.ProductId});
            
            builder.Property(x => x.Quantity)
                .IsRequired() // NOT NULL
                .HasColumnName("Quantity") //NOME DA COLUNA
                .HasColumnType("float"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.Price)
                .IsRequired() // NOT NULL
                .HasColumnName("Price") //NOME DA COLUNA
                .HasColumnType("float"); //TIPO DE DADO DA COLUNA

        }
    }
}