using Example_of_Entityframework_Core.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Example_of_Entityframework_Core.DataAccess
{
    public class EntityDBContext : DbContext
    {
        public EntityDBContext(DbContextOptions<EntityDBContext> options) : base(options) { }

        public DbSet<Libro>? Libros { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Categorias>? Categorias { get; set; }
        public DbSet<CategoriaLibro>? CategoriaLibros { get; set; }
        public DbSet<UsuariosAntiguos>? UsuariosAntiguos { get; set; }
        public DbSet<GrantedUser>? GrantedUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Libro>();

            builder.Entity<Usuario>();

            builder.Entity<Categorias>();

            builder.Entity<GrantedUser>();

            builder.Entity<CategoriaLibro>()
                .HasKey(cl => new {cl.LibroId, cl.CategoriaId});

            builder.Entity<CategoriaLibro>()
                .HasOne(cl => cl.Libro)
                .WithMany(l => l.CategoriaLibros)
                .HasForeignKey(cl => cl.LibroId);

            builder.Entity<CategoriaLibro>()
                .HasOne(cl => cl.Categoria)
                .WithMany(c => c.CategoriaLibros)
                .HasForeignKey(cl => cl.CategoriaId);


            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            Usuario sp1 = new Usuario()
            {
                UsuarioId = -1,
                Nombre = "Usu1"
            };

            Usuario sp2 = new Usuario()
            {
                UsuarioId = -2,
                Nombre = "Usu2"
            };

            Usuario sp3 = new Usuario()
            {
                UsuarioId = -3,
                Nombre = "Usu3"
            };

            Libro tk1 = new Libro()
            {
                LibroId = -1,
                Titulo = "tit1",
                Autor = "aut1",
                UsuarioId = -1
            };

            Libro tk2 = new Libro()
            {
                LibroId = -2,
                Titulo = "tit2",
                Autor = "aut2",
                UsuarioId = -1
            };

            Libro tk3 = new Libro()
            {
                LibroId = -3,
                Titulo = "tit3",
                Autor = "aut3",
                UsuarioId = -2
            };

            Libro tk4 = new Libro()
            {
                LibroId = -4,
                Titulo = "tit4",
                Autor = "aut4",
                UsuarioId = -3
            };

            Libro tk5 = new Libro()
            {
                LibroId = -5,
                Titulo = "tit5",
                Autor = "aut5",
                UsuarioId = -3
            };

            Libro tk6 = new Libro()
            {
                LibroId = -6,
                Titulo = "tit6",
                Autor = "aut6",
                UsuarioId = -3
            };

            Categorias cat1 = new Categorias()
            {
                CategoriasId = -1,
                Categoria = "Cat1"
            };

            Categorias cat2 = new Categorias()
            {
                CategoriasId = -2,
                Categoria = "Cat2"
            };

            Categorias cat3 = new Categorias()
            {
                CategoriasId = -3,
                Categoria = "Cat3"
            };

            Categorias cat4 = new Categorias()
            {
                CategoriasId = -4,
                Categoria = "Cat4"
            };

            CategoriaLibro cl1 = new CategoriaLibro()
            {
                CategoriaLibroId = -1,
                CategoriaId = -1,
                LibroId = -1
            };

            CategoriaLibro cl2 = new CategoriaLibro()
            {
                CategoriaLibroId = -2,
                CategoriaId = -1,
                LibroId = -2
            };

            CategoriaLibro cl3 = new CategoriaLibro()
            {
                CategoriaLibroId = -3,
                CategoriaId = -2,
                LibroId = -4
            };

            CategoriaLibro cl4 = new CategoriaLibro()
            {
                CategoriaLibroId = -4,
                CategoriaId = -3,
                LibroId = -5
            };

            CategoriaLibro cl5 = new CategoriaLibro()
            {
                CategoriaLibroId = -5,
                CategoriaId = -4,
                LibroId = -5
            };

            CategoriaLibro cl6 = new CategoriaLibro()
            {
                CategoriaLibroId = -6,
                CategoriaId = -4,
                LibroId = -6
            };

            CategoriaLibro cl7 = new CategoriaLibro()
            {
                CategoriaLibroId = -7,
                CategoriaId = -4,
                LibroId = -3
            };

            GrantedUser gu1 = new GrantedUser()
            {
                GrantedUserId = 1,
                Email = "gonzalo@prueba.es",
                Name = "Admin",
                LastName = "de la predera",
                Password = "Admin"
            };

            GrantedUser gu2 = new GrantedUser()
            {
                GrantedUserId = 2,
                Email = "pepe@prueba.es",
                Name = "User 1",
                LastName = "Lolailo",
                Password = "pepe"
            };

            builder.Entity<Usuario>().HasData(sp1, sp2, sp3);
            builder.Entity<Libro>().HasData(tk1, tk2, tk3, tk4, tk5, tk6);
            builder.Entity<Categorias>().HasData(cat1, cat2, cat3, cat4);
            builder.Entity<CategoriaLibro>().HasData(cl1, cl2, cl3, cl4, cl5, cl6, cl7);
        }


    }
}
