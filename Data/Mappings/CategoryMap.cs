using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Tabela
            builder.ToTable("Category");
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
                .HasMaxLength(80); //TAMANHO DO CAMPO

            // builder.Property(x => x.Slug)
            //     .IsRequired() // NOT NULL
            //     .HasColumnName("Slug") //NOME DA COLUNA
            //     .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
            //     .HasMaxLength(80); //TAMANHO DO CAMPO

            //Indíces
            builder.HasIndex(x => x.Title, "IX_Category_Title")
                .IsUnique(); //Indice é único


        }
    }
}