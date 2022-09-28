using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;

namespace Example_of_Entityframework_Core.Controllers
{
    /***************
     * 
     * TODO: Desarrollar GETs para
     *      Libros por categoria
     *      Libros de una categoria
     *      Categorias de un libro
     *      
     *************************************/

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaLibroesController : ControllerBase
    {
        private readonly EntityDBContext _context;

        public CategoriaLibroesController(EntityDBContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaLibroes
        // Libros por categoría => toda las categorías con sus libros

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaLibro>>> GetUsuarios()
        {
            var categorias = await _context.Categorias.ToListAsync();
            var libros = await _context.Libros.ToListAsync();
            var catLibros = await _context.CategoriaLibros.ToListAsync();
            IEnumerable<Categorias> result;

            var query = from cl in catLibros
                        join cat in categorias
                        on cl.CategoriaId equals cat.CategoriasId
                        join lib in libros
                        on cl.LibroId equals lib.LibroId
                        select new CategoriaLibro()
                        {
                            Categoria = new Categorias()
                            {
                                CategoriasId = cat.CategoriasId,
                                Categoria = cat.Categoria
                            },

                            Libro = new Libro()
                            {
                                LibroId = lib.LibroId,
                                Titulo = lib.Titulo,
                                Autor = lib.Autor
                            }
                        };

            return query.ToArray();
        }

        // GET: api/CategoriaLibroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaLibro>> GetCategoriaLibro(int id)
        {
            var categoriaLibro = await _context.CategoriaLibros.FindAsync(id);

            if (categoriaLibro == null)
            {
                return NotFound();
            }

            return categoriaLibro;
        }

        // PUT: api/CategoriaLibroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaLibro(int id, CategoriaLibro categoriaLibro)
        {
            if (id != categoriaLibro.LibroId)
            {
                return BadRequest();
            }

            _context.Entry(categoriaLibro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaLibroExists(id))
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

        // POST: api/CategoriaLibroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaLibro>> PostCategoriaLibro(CategoriaLibro categoriaLibro)
        {
            _context.CategoriaLibros.Add(categoriaLibro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoriaLibroExists(categoriaLibro.LibroId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategoriaLibro", new { id = categoriaLibro.LibroId }, categoriaLibro);
        }

        // DELETE: api/CategoriaLibroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaLibro(int id)
        {
            var categoriaLibro = await _context.CategoriaLibros.FindAsync(id);
            if (categoriaLibro == null)
            {
                return NotFound();
            }

            _context.CategoriaLibros.Remove(categoriaLibro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaLibroExists(int id)
        {
            return _context.CategoriaLibros.Any(e => e.LibroId == id);
        }
    }
}
