﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signo.Data.Context;

namespace Signo.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    partial class MeuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Signo.Business.Models.Integrante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Admissao")
                        .HasColumnType("datetime");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("BairroEndereco")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CepEndereco")
                        .HasColumnType("varchar(8)");

                    b.Property<string>("ComplementoEndereco")
                        .HasColumnType("varchar(350)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FuncaoBordo")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FuncaoContrato")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<string>("ImgFoto")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("ImgSign")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("LocalidadeEndereco")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("LogradouroEndereco")
                        .HasColumnType("varchar(350)");

                    b.Property<string>("Matricula")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NumeroEndereco")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UfEndereco")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Unidade")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Integrantes");
                });
#pragma warning restore 612, 618
        }
    }
}
