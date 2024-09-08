﻿// <auto-generated />
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_CONTATO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("EMAIL")
                        .HasAnnotation("RegularExpression", "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME_COMPLETO");

                    b.Property<int>("TelefoneDdd")
                        .HasMaxLength(2)
                        .HasColumnType("INT")
                        .HasColumnName("TELEFONE_DDD");

                    b.Property<string>("TelefoneNum")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)")
                        .HasColumnName("TELEFONE_NUMERO");

                    b.HasKey("Id");

                    b.HasIndex("TelefoneDdd");

                    b.ToTable("CONTATO", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Regiao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_REGIAO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME_REGIAO");

                    b.Property<int>("RegiaoDdd")
                        .HasColumnType("INT")
                        .HasColumnName("DDD_REGIAO");

                    b.HasKey("Id");

                    b.ToTable("REGIAO", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Contato", b =>
                {
                    b.HasOne("Domain.Entity.Regiao", "Regiao")
                        .WithMany("Contatos")
                        .HasForeignKey("TelefoneDdd")
                        .HasPrincipalKey("RegiaoDdd")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("Domain.Entity.Regiao", b =>
                {
                    b.Navigation("Contatos");
                });
#pragma warning restore 612, 618
        }
    }
}
