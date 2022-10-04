﻿// <auto-generated />
using System;
using Example_of_Entityframework_Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Example_of_Entityframework_Core.Migrations
{
    [DbContext(typeof(EntityDBContext))]
    partial class EntityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.CategoriaLibro", b =>
                {
                    b.Property<int>("LibroId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaLibroId")
                        .HasColumnType("int");

                    b.HasKey("LibroId", "CategoriaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("CategoriaLibros");

                    b.HasData(
                        new
                        {
                            LibroId = -1,
                            CategoriaId = -1,
                            CategoriaLibroId = -1
                        },
                        new
                        {
                            LibroId = -2,
                            CategoriaId = -1,
                            CategoriaLibroId = -2
                        },
                        new
                        {
                            LibroId = -4,
                            CategoriaId = -2,
                            CategoriaLibroId = -3
                        },
                        new
                        {
                            LibroId = -5,
                            CategoriaId = -3,
                            CategoriaLibroId = -4
                        },
                        new
                        {
                            LibroId = -5,
                            CategoriaId = -4,
                            CategoriaLibroId = -5
                        },
                        new
                        {
                            LibroId = -6,
                            CategoriaId = -4,
                            CategoriaLibroId = -6
                        },
                        new
                        {
                            LibroId = -3,
                            CategoriaId = -4,
                            CategoriaLibroId = -7
                        });
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Categorias", b =>
                {
                    b.Property<int>("CategoriasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriasId"), 1L, 1);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoriasId");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            CategoriasId = -1,
                            Categoria = "Cat1"
                        },
                        new
                        {
                            CategoriasId = -2,
                            Categoria = "Cat2"
                        },
                        new
                        {
                            CategoriasId = -3,
                            Categoria = "Cat3"
                        },
                        new
                        {
                            CategoriasId = -4,
                            Categoria = "Cat4"
                        });
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.GrantedUser", b =>
                {
                    b.Property<int>("GrantedUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GrantedUserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GrantedUserId");

                    b.ToTable("GrantedUsers");

                    b.HasData(
                        new
                        {
                            GrantedUserId = 1,
                            Email = "gonzalo@prueba.es",
                            LastName = "de la predera",
                            Name = "Admin",
                            Password = "Admin"
                        },
                        new
                        {
                            GrantedUserId = 2,
                            Email = "pepe@prueba.es",
                            LastName = "Lolailo",
                            Name = "User 1",
                            Password = "pepe"
                        });
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Libro", b =>
                {
                    b.Property<int>("LibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibroId"), 1L, 1);

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("LibroId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Libros");

                    b.HasData(
                        new
                        {
                            LibroId = -1,
                            Autor = "aut1",
                            Titulo = "tit1",
                            UsuarioId = -1
                        },
                        new
                        {
                            LibroId = -2,
                            Autor = "aut2",
                            Titulo = "tit2",
                            UsuarioId = -1
                        },
                        new
                        {
                            LibroId = -3,
                            Autor = "aut3",
                            Titulo = "tit3",
                            UsuarioId = -2
                        },
                        new
                        {
                            LibroId = -4,
                            Autor = "aut4",
                            Titulo = "tit4",
                            UsuarioId = -3
                        },
                        new
                        {
                            LibroId = -5,
                            Autor = "aut5",
                            Titulo = "tit5",
                            UsuarioId = -3
                        },
                        new
                        {
                            LibroId = -6,
                            Autor = "aut6",
                            Titulo = "tit6",
                            UsuarioId = -3
                        });
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = -1,
                            Nombre = "Usu1"
                        },
                        new
                        {
                            UsuarioId = -2,
                            Nombre = "Usu2"
                        },
                        new
                        {
                            UsuarioId = -3,
                            Nombre = "Usu3"
                        });
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.UsuariosAntiguos", b =>
                {
                    b.Property<int>("UsuariosAntiguosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuariosAntiguosId"), 1L, 1);

                    b.Property<int?>("LibroAntiguoId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioAntiguoUsuarioId")
                        .HasColumnType("int");

                    b.HasKey("UsuariosAntiguosId");

                    b.HasIndex("UsuarioAntiguoUsuarioId");

                    b.ToTable("UsuariosAntiguos");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.CategoriaLibro", b =>
                {
                    b.HasOne("Example_of_Entityframework_Core.Models.DataModels.Categorias", "Categoria")
                        .WithMany("CategoriaLibros")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Example_of_Entityframework_Core.Models.DataModels.Libro", "Libro")
                        .WithMany("CategoriaLibros")
                        .HasForeignKey("LibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Libro");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Libro", b =>
                {
                    b.HasOne("Example_of_Entityframework_Core.Models.DataModels.Usuario", null)
                        .WithMany("Libros")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.UsuariosAntiguos", b =>
                {
                    b.HasOne("Example_of_Entityframework_Core.Models.DataModels.Usuario", "UsuarioAntiguo")
                        .WithMany()
                        .HasForeignKey("UsuarioAntiguoUsuarioId");

                    b.Navigation("UsuarioAntiguo");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Categorias", b =>
                {
                    b.Navigation("CategoriaLibros");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Libro", b =>
                {
                    b.Navigation("CategoriaLibros");
                });

            modelBuilder.Entity("Example_of_Entityframework_Core.Models.DataModels.Usuario", b =>
                {
                    b.Navigation("Libros");
                });
#pragma warning restore 612, 618
        }
    }
}
