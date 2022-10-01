using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;
using System.Text.Json.Serialization;
using System.Text.Json;
using Example_of_Entityframework_Core.Models.ResultModels;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroesController : ControllerBase
    {
        private readonly EntityDBContext _context;

        public LibroesController(EntityDBContext context)
        {
            _context = context;
        }

        // GET: api/Libroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            var libros = await _context.Libros.ToListAsync();

            JsonSerializerOptions option = new()
            {

                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

            };

            var stringresult = JsonSerializer.Serialize(libros, option);


            return libros
                ;
        }

        // GET: api/LibrosPorCategorias/5
        // Gets all the books in a Category
        [HttpGet]
        [Route("api/CategoriasDeLibro/{LibroId}")]
        public async Task<ActionResult<CategoriasDeLibro>> GetCategoriasDeLibro(int LibroId)
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

        // GET: api/Libroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // GET: api/UsuariosDeLibros/5
        [HttpGet("api/UsuariosDeLibro/{id}")]
        public async Task<ActionResult<ICollection<UsuarioBasico>>> GetUsuariosDeLibro(int id)
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
            

            foreach(var au in requestAnt)
            {
               request = request.Append(au);
               
            }

            return request.ToArray();
        }

        // PUT: api/Libroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/ModificarLibro/{id}")]
        public async Task<IActionResult> PutLibro(int id, LibroBasico lib)
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

        //Añadir Libro a Categoria
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/AñadirCategoriaALibro/{CategoriaId}")]
        public async Task<IActionResult> PutLibroEnCategorias(int LibroId, int CategoriaId)
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

        // DELETE: api/Libroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
