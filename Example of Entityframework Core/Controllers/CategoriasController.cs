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
using Example_of_Entityframework_Core.Services;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly EntityDBContext _context;
        private readonly ICategoriaServices _catService;

        public CategoriasController(EntityDBContext context, ICategoriaServices catService)
        {
            _context = context;
            _catService = catService;
        }

        // GET: api/Categorias
        [HttpGet("Todas las Categorías")]
        public async Task<IEnumerable<Categorias>> GetCategorias()
        {
            return await _catService.GetCategoriasService();
        }

        // GET: api/LibrosPorCategorias/5
        // Gets all the books in a Category
        [HttpGet("Libros Por Categoria/{categoriaId}")]
        public async Task<ActionResult<LibrosPorCategoria>> GetLibrosPorCategoria(int categoriaId)
        {
            return await _catService.GetLibrosPorCategoriaService(categoriaId);
        }

        // GET: api/Categorias/5
        [HttpGet("Categoría Por Id/{id}")]
        public async Task<ActionResult<Categorias>> GetCategoriasPorId(int id)
        {
           return await _catService.GetCategoriaPorIdService(id);
        }

        //Modificar Categoria
        [HttpPut("Modificar Categoría/{id}")]
        public async Task<IActionResult> PutModificarCategoria(int catId, CategoriaBasica cat)
        {
            return await _catService.PutModificarCategoriaService(catId, cat);
        }

        //Añadir Libro a Categoria
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Añadir Libro A Categoria/{CategoriaId}")]
        public async Task<IActionResult> PutLibroEnCategorias(int CategoriaId, int LibroId)
        {
            return await _catService.PutLibroEnCategoriasService(CategoriaId, LibroId);
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Añadir Categoría")]
        public async Task<ActionResult<Categorias>> PostCategorias(CategoriaBasica cat)
        {
            return await _catService.PostCategoriasService(cat);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("Borrar Categoría/{id}")]
        public async Task<IActionResult> DeleteCategorias(int id)
        {
            return await _catService.DeleteCategoriasService(id);
        }
    }
}
