using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly EntityDBContext _context;

        public CategoriasController(EntityDBContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/LibrosPorCategorias/5
        // Gets all the books in a Category
        [HttpGet]
        [Route("api/LibrosPorCategoria/{categoriaId}")]
        public async Task<ActionResult<LibrosPorCategoria>> GetLibrosPorCategoria(int categoriaId)
        {
            var categoria = await _context.Categorias.FindAsync(categoriaId);
            var libros = await _context.Libros.ToListAsync();
            var catlib = await _context.CategoriaLibros.ToListAsync();

            //Avoid circular reference
            JsonSerializerOptions opt = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            if (categoria == null)
            {
                return NotFound();
            }

            var librosEnCategoria = from cl in catlib
                         where cl.CategoriaId == categoriaId
                         select cl.Libro;

            LibrosPorCategoria result = new LibrosPorCategoria()
            {
                CategoriaId = categoria.CategoriasId,
                Categoria = categoria.Categoria,
                Libros = new List<LibroBasico>()
            };

            foreach (Libro libro in librosEnCategoria)
            {
            
                LibroBasico newLib = new()
                {
                    LibroId = libro.LibroId,
                    Titulo = libro.Titulo,
                    Autor = libro.Autor
                };

                result.Libros.Add(newLib);
                
            }

            // Kill circular references
            string jsonStr = JsonSerializer.Serialize(result, opt);
            LibrosPorCategoria? lpc = JsonSerializer.Deserialize<LibrosPorCategoria>(jsonStr, opt);

            return lpc;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategorias(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);

            if (categorias == null)
            {
                return NotFound();
            }

            return categorias;
        }

        //Añadir Libro a Categoria
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/AñadirLibroACategoria/{CategoriaId}")]
        public async Task<IActionResult> PutLibroEnCategorias(int CategoriaId, int LibroId)
        {
            var cat = await _context.Categorias.FindAsync(CategoriaId);
            var lib = await _context.Libros.FindAsync(LibroId);
            var catLib = await _context.CategoriaLibros.ToListAsync();

            if (cat == null || lib == null) return NotFound();

            CategoriaLibro categoriaLibro = new CategoriaLibro()
            {
                CategoriaLibroId = catLib.Count+1,
                LibroId = LibroId,
                CategoriaId = CategoriaId,
                Libro = lib,
                Categoria = cat
            };        

            _context.CategoriaLibros.Add(categoriaLibro);

            await _context.SaveChangesAsync();
           

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategorias(CategoriaBasica cat)
        {
            Categorias categoria = new()
            {
                CategoriasId = cat.CategoriaId,
                Categoria = cat.Categoria
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategorias", new { id = categoria.CategoriasId }, categoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorias(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categorias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriasId == id);
        }
    }
}
