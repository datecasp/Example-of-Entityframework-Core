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
using Example_of_Entityframework_Core.Services;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroesController : ControllerBase
    {
        private readonly EntityDBContext _context;
        private readonly ILibroServices _libroServices;

        public LibroesController(EntityDBContext context, ILibroServices libroServices)
        {
            _context = context;
            _libroServices = libroServices;
        }

        // GET: api/Libroes
        [HttpGet("Todos Los Libros")]
        public async Task<IEnumerable<Libro>> GetLibros()
        {
            return await _libroServices.GetLibrosService();
        }

        // GET: api/LibrosPorCategorias/5
        // Gets all the books in a Category
        [HttpGet]
        [Route("Categorias De Libro/{LibroId}")]
        public async Task<ActionResult<CategoriasDeLibro>> GetCategoriasDeLibro(int LibroId)
        {
            return await _libroServices.GetCategoriasDeLibroService(LibroId);  
        }

        // GET: api/Libroes/5
        [HttpGet("Libro Por Id/{id}")]
        public async Task<ActionResult<Libro>> GetLibroPorId(int id)
        {
            return await _libroServices.GetLibroPorIdService(id);
        }

        // GET: api/UsuariosDeLibros/5
        [HttpGet("Usuarios De Libro/{id}")]
        public async Task<ActionResult<ICollection<UsuarioBasico>>> GetUsuariosDeLibro(int id)
        {
            return await _libroServices.GetUsuariosDeLibroService(id);
        }

        // PUT: api/Libroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Modificar Libro/{id}")]
        public async Task<IActionResult> PutModificarLibro(int id, LibroBasico lib)
        {
            return await _libroServices.PutModificarLibroService(id, lib);
        }

        //Añadir Libro a Categoria
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Añadir Categoria A Libro/{CategoriaId}")]
        public async Task<IActionResult> PutLibroEnCategorias(int LibroId, int CategoriaId)
        {
            return await _libroServices.PutLibroEnCategoriasService(LibroId, CategoriaId);
        }

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Añadir Libro")]
        public async Task<ActionResult<LibroBasico>> PostCategorias(LibroBasico lib)
        {
            return await _libroServices.PostLibroService(lib);
        }

        // DELETE: api/Libroes/5
        [HttpDelete("Borrar Libro/{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            return await _libroServices.DeleteLibroService(id);
        }

        
    }
}
