using Example_of_Entityframework_Core.Helpers;
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

            DBSeeder.Seed(builder);
        }
    }
}
