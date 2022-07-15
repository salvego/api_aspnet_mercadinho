using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Tabela
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            
            builder.Property(x => x.CreatedDateTime)
                .IsRequired() // NOT NULL
                .HasColumnName("CreatedDateTime") //NOME DA COLUNA
                .HasColumnType("datetime"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.OverdueDateTime)
                .IsRequired(false) // NOT NULL
                .HasColumnName("OverdueDateTime") //NOME DA COLUNA
                .HasColumnType("datetime"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.StatusOrder)
                .IsRequired() // NOT NULL
                .HasColumnName("StatusOrder") //NOME DA COLUNA
                .HasColumnType("varchar") //TIPO DE DADO DA COLUNA
                .HasMaxLength(60); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.CopyAndPaste)
                .IsRequired(false) // NULL
                .HasColumnName("CopyAndPaste") //NOME DA COLUNA
                .HasColumnType("varchar")
                .HasMaxLength(200); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.Total)
                .IsRequired() // NOT NULL
                .HasColumnName("Total") //NOME DA COLUNA
                .HasColumnType("float");

        }    
    }
}   