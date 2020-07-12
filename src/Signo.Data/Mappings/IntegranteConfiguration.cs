using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signo.Business.Models;

namespace Signo.Data.Mappings
{
    public class IntegranteConfiguration : IEntityTypeConfiguration<Integrante>
    {
        public void Configure(EntityTypeBuilder<Integrante> builder)
        {
            builder.HasKey(p => p.Id);


            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");


            builder.Property(p => p.Unidade)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(p => p.Matricula)
                .IsRequired(false)
               .HasColumnType("varchar(50)");

            builder.Property(p => p.FuncaoBordo)
                .IsRequired()
                .HasColumnType("varchar(150)");


            builder.Property(p => p.FuncaoContrato)
                .IsRequired()
                .HasColumnType("varchar(150)");


            builder.Property(p => p.Empresa)
                .IsRequired()
                .HasColumnType("varchar(150)");


           

            builder.Property(p => p.CepEndereco)

                .HasColumnType("varchar(8)");

            builder.Property(p => p.LogradouroEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(350)");

            builder.Property(p => p.BairroEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(200)");

            builder.Property(p => p.LocalidadeEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(200)");

            builder.Property(p => p.UfEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.NumeroEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(150)");

            builder.Property(p => p.ComplementoEndereco)
                .IsRequired(false)
                .HasColumnType("varchar(350)");

            builder.Property(p => p.Telefone)
                    .IsRequired(false)
                    .HasColumnType("varchar(150)");


            builder.Property(p => p.ImgFoto)
                .IsRequired()
                .HasColumnType("varchar(300)");


            builder.Property(p => p.ImgSign)
                .IsRequired()
                .HasColumnType("varchar(300)");


            builder.Property(p => p.Nacionalidade)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(p => p.Admin)
                .IsRequired()
                .HasColumnType("bit");


            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasColumnType("bit");


            builder.Property(p => p.Admissao)

                .HasColumnType("datetime");

            builder.Property(p => p.DoB)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Genero)
                .IsRequired()
                .HasColumnType("int");



            builder.ToTable("Integrantes");

        }
    }
}
