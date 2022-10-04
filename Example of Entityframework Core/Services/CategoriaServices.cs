using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Example_of_Entityframework_Core.Services
{
    public class CategoriaServices : ControllerBase, ICategoriaServices
    {
        private EntityDBContext _context;

        public CategoriaServices(EntityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categorias>> GetCategoriasService()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<ActionResult<LibrosPorCategoria>> GetLibrosPorCategoriaService(int categoriaId)
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

        public async Task<ActionResult<Categorias>> GetCategoriaPorIdService(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);

            if (categorias == null)
            {
                return NotFound();
            }

            return categorias;
        }

        public async Task<IActionResult> PutModificarCategoriaService(int catId, CategoriaBasica cat)
        {
            if (catId != cat.CategoriaId)
            {
                return BadRequest();
            }

            CategoriaBasica newCat = new CategoriaBasica()
            {
                CategoriaId = catId,
                Categoria = cat.Categoria
            };

            _context.Entry(newCat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(catId))
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
        public async Task<IActionResult> PutLibroEnCategoriasService(int CategoriaId, int LibroId)
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

        public async Task<ActionResult<Categorias>> PostCategoriasService(CategoriaBasica cat)
        {
            Categorias categoria = new()
            {
                CategoriasId = cat.CategoriaId,
                Categoria = cat.Categoria
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<IActionResult> DeleteCategoriasService(int id)
        {
            /***********************************
             * 
             *      Implementar dar de baja a categoría
             *      
             *      en lugar de borrarlo, creando un
             *      
             *      campo nuevo fecha de baja.
             *      
             *      Si el campo es NULL, está activo
             *      
             *      si no, está de baja y NO DEBERÏA
             *      
             *      poder tener libros (flag)
             * 
            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categorias);
            await _context.SaveChangesAsync();
            */
            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriasId == id);
        }
    }
}
