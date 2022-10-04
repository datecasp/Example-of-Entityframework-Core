using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Example_of_Entityframework_Core.Models.ResultModels;

namespace Example_of_Entityframework_Core.Services
{
    public class LibroServices : ControllerBase, ILibroServices
    {
        private EntityDBContext _context;

        public LibroServices(EntityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> GetLibrosService()
        {
            var libros = await _context.Libros.ToListAsync();

            JsonSerializerOptions option = new()
            {

                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

            };

            var stringresult = JsonSerializer.Serialize(libros, option);


            return libros;
        }

        public async Task<ActionResult<CategoriasDeLibro>> GetCategoriasDeLibroService(int LibroId)
        {
            var categorias = await _context.Categorias.ToListAsync();
            var libro = await _context.Libros.FindAsync(LibroId);
            var catlib = await _context.CategoriaLibros.ToListAsync();

            //Avoid circular reference
            JsonSerializerOptions opt = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            if (libro == null)
            {
                return NotFound();
            }

            var catDeLib = from cl in catlib
                           where cl.LibroId == LibroId
                           select cl.Categoria;

            CategoriasDeLibro result = new CategoriasDeLibro()
            {
                LibroId = libro.LibroId,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Categorias = new List<CategoriaBasica>()
            };

            foreach (Categorias cat in catDeLib)
            {

                CategoriaBasica newCat = new()
                {
                    CategoriaId = cat.CategoriasId,
                    Categoria = cat.Categoria
                };

                result.Categorias.Add(newCat);
            }

            // Kill circular references
            string jsonStr = JsonSerializer.Serialize(result, opt);
            CategoriasDeLibro? lpc = JsonSerializer.Deserialize<CategoriasDeLibro>(jsonStr, opt);

            return lpc;
        }

        public async Task<ActionResult<Libro>> GetLibroPorIdService(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        public async Task<ActionResult<ICollection<UsuarioBasico>>> GetUsuariosDeLibroService(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            var usuarios = await _context.Usuarios.ToListAsync();
            var antUsuarios = await _context.UsuariosAntiguos.ToListAsync();

            if (libro == null)
            {
                return NotFound();
            }

            var request = from usu in usuarios
                          where usu.UsuarioId == libro.UsuarioId
                          select new UsuarioBasico()
                          {
                              UsuarioId = usu.UsuarioId,
                              Nombre = usu.Nombre
                          };

            var requestAnt = from au in antUsuarios
                             where au.LibroAntiguoId == id
                             select new UsuarioBasico()
                             {
                                 UsuarioId = au.UsuarioAntiguo.UsuarioId,
                                 Nombre = au.UsuarioAntiguo.Nombre
                             };


            foreach (var au in requestAnt)
            {
                request = request.Append(au);

            }

            return request.ToArray();
        }

        public async Task<IActionResult> PutModificarLibroService(int id, LibroBasico lib)
        {
            if (id != lib.LibroId)
            {
                return BadRequest();
            }

            Libro libro = new Libro()
            {
                LibroId = id,
                Titulo = lib.Titulo,
                Autor = lib.Autor
            };


            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<IActionResult> PutLibroEnCategoriasService(int LibroId, int CategoriaId)
        {
            var cat = await _context.Categorias.FindAsync(CategoriaId);
            var lib = await _context.Libros.FindAsync(LibroId);
            var catLib = await _context.CategoriaLibros.ToListAsync();

            if (cat == null || lib == null) return NotFound();

            CategoriaLibro categoriaLibro = new CategoriaLibro()
            {
                CategoriaLibroId = catLib.Count + 1,
                LibroId = LibroId,
                CategoriaId = CategoriaId,
                Libro = lib,
                Categoria = cat
            };

            _context.CategoriaLibros.Add(categoriaLibro);

            await _context.SaveChangesAsync();


            return NoContent();
        }

        public async Task<ActionResult<LibroBasico>> PostLibroService(LibroBasico libro)
        {
            Libro lib = new()
            {
                LibroId = libro.LibroId,
                Titulo = libro.Titulo,
                Autor = libro.Autor
            };
            _context.Libros.Add(lib);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<IActionResult> DeleteLibroService(int id)
        {
            /***********************************
             * 
             *      Implementar dar de baja a libro
             *      
             *      en lugar de borrarlo, creando un
             *      
             *      campo nuevo fecha de baja.
             *      
             *      Si el campo es NULL, está activo
             *      
             *      si no, está de baja y NO DEBERÏA
             *      
             *      poder ser cogido por Usuarios
             *      
             *      ni estar en cateforías (flag)
             * 
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            */
            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
