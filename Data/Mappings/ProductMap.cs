using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Tabela
            builder.ToTable("Product");
            //Chave-Primary
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsRequired() // NOT NULL
                .HasColumnName("Title") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(120); //TAMANHO DO CAMPO

            builder.Property(x => x.Picture)
                .HasColumnName("Picture") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(80); //TAMANHO DO CAMPO

            builder.Property(x => x.Unit)
                .HasColumnName("Unit") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(80); //TAMANHO DO CAMPO

            builder.Property(x => x.Price)
                .IsRequired() // NOT NULL
                .HasColumnName("Price") //NOME DA COLUNA
                .HasColumnType("FLOAT"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.Description)
                .HasColumnName("Description") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(120); //TAMANHO DO CAMPO
                
            //Indíces
            builder.HasIndex(x => x.Title, "IX_Product_Title")
                .IsUnique(); //Indice é único

        }
    }
}