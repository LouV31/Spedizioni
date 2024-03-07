﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spedizioni.Data;

#nullable disable

namespace Spedizioni.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240307132010_UpdateForeignKeyDettagliSpedizione")]
    partial class UpdateForeignKeyDettagliSpedizione
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Spedizioni.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceFiscale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartitaIva")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("Spedizioni.Models.DettagliSpedizione", b =>
                {
                    b.Property<int>("IdDettagliSpedizione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDettagliSpedizione"));

                    b.Property<int>("IdSpedizione")
                        .HasColumnType("int");

                    b.Property<string>("Stato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDettagliSpedizione");

                    b.HasIndex("IdSpedizione");

                    b.ToTable("DettagliSpedizione");
                });

            modelBuilder.Entity("Spedizioni.Models.Spedizione", b =>
                {
                    b.Property<int>("IdSpedizione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSpedizione"));

                    b.Property<DateTime>("DataConsegna")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSpedizione")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destinazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<string>("Indirizzo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NominativoDestinatario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrezzoSpedizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("peso")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdSpedizione");

                    b.HasIndex("IdCliente");

                    b.ToTable("Spedizione");
                });

            modelBuilder.Entity("Spedizioni.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Spedizioni.Models.DettagliSpedizione", b =>
                {
                    b.HasOne("Spedizioni.Models.Spedizione", "Spedizione")
                        .WithMany("DettagliSpedizione")
                        .HasForeignKey("IdSpedizione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spedizione");
                });

            modelBuilder.Entity("Spedizioni.Models.Spedizione", b =>
                {
                    b.HasOne("Spedizioni.Models.Cliente", "Cliente")
                        .WithMany("Spedizioni")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Spedizioni.Models.Cliente", b =>
                {
                    b.Navigation("Spedizioni");
                });

            modelBuilder.Entity("Spedizioni.Models.Spedizione", b =>
                {
                    b.Navigation("DettagliSpedizione");
                });
#pragma warning restore 612, 618
        }
    }
}
