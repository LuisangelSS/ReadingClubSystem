﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReadingClubSystem.Api.Data;

#nullable disable

namespace ReadingClubSystem.Api.Migrations
{
    [DbContext(typeof(ReadingClubContext))]
    partial class ReadingClubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClubUsuario", b =>
                {
                    b.Property<int>("ClubesId")
                        .HasColumnType("int");

                    b.Property<int>("MiembrosId")
                        .HasColumnType("int");

                    b.HasKey("ClubesId", "MiembrosId");

                    b.HasIndex("MiembrosId");

                    b.ToTable("ClubUsuario");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clubes");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Recomendacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("LibroId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("LibroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Recomendaciones");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Reunion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Reuniones");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ClubUsuario", b =>
                {
                    b.HasOne("ReadingClubSystem.Domain.Entities.Club", null)
                        .WithMany()
                        .HasForeignKey("ClubesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReadingClubSystem.Domain.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("MiembrosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Recomendacion", b =>
                {
                    b.HasOne("ReadingClubSystem.Domain.Entities.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReadingClubSystem.Domain.Entities.Libro", "Libro")
                        .WithMany("Recomendaciones")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReadingClubSystem.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("Libro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Reunion", b =>
                {
                    b.HasOne("ReadingClubSystem.Domain.Entities.Club", "Club")
                        .WithMany("Reuniones")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Club", b =>
                {
                    b.Navigation("Reuniones");
                });

            modelBuilder.Entity("ReadingClubSystem.Domain.Entities.Libro", b =>
                {
                    b.Navigation("Recomendaciones");
                });
#pragma warning restore 612, 618
        }
    }
}