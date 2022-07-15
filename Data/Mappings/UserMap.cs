using Mercadinho.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mercadinho.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Tabela
            builder.ToTable("User");
            //Chave-Primary
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired() // NOT NULL
                .HasColumnName("Name") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(80); //TAMANHO DO CAMPO

            builder.Property(x => x.Email)
                .IsRequired() // NOT NULL
                .HasColumnName("Email") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(160); //TAMANHO DO CAMPO
            builder.Property(x => x.Phone)
                .IsRequired(false) // NULL
                .HasColumnName("Phone") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(20); //TAMANHO DO CAMPO
            builder.Property(x => x.CPF)
                .IsRequired(false) // NOT NULL
                .HasColumnName("CPF") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(14); //TAMANHO DO CAMPO
            builder.Property(x => x.Password)
                .IsRequired() // NOT NULL
                .HasColumnName("Password") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(255); //TAMANHO DO CAMPO
            builder.Property(x => x.Bio)
                .IsRequired(false);// NULL;
            builder.Property(x => x.Image)
                .IsRequired(false);// NULL;

            builder.Property(x => x.Slug)
                .IsRequired() // NOT NULL
                .HasColumnName("Slug") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(80); //TAMANHO DO CAMPO

            //Indíces
            builder.HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique(); //Indice é único


            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>
                (
                    "UserRole",
                    role => role.HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user.HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

        }
    }
}