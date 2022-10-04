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
using Example_of_Entityframework_Core.Services;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly EntityDBContext _context;
        private readonly IUsuarioServices _userServices;

        public UsuariosController(EntityDBContext context, IUsuarioServices usuServices)
        {
            _context = context;
            _userServices = usuServices;
        }

        // GET: api/Usuarios
        [HttpGet("Todos Los Usuarios")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return await _userServices.GetUsersService();
        }

        // GET: api/Usuarios/5
        [HttpGet("Usuario Por Id/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            return await _userServices.GetUsuarioPorIdService(id);
        }

        // GET: api/LibrosDeUsuario/5
        //  Get the books of a user
        [HttpGet]
        [Route("Libros De Usuario/{UsuarioId}")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosDeUsuario(int UsuarioId)
        {
            return await _userServices.GetLibrosDeUsuarioService(UsuarioId);
        }

        // PUT: api/UsuarioDevuelveLibro/
        [HttpPut("Usuario Devuelve Libro/{UsuarioId}")]
        public async Task<IActionResult> PutUsuarioDevuelveLibro(int UsuarioId, int LibroId)
        {
            return await _userServices.PutUsuarioDevuelveLibroService(UsuarioId, LibroId);
        }

        // PUT: api/UsuarioCogeLibro/
        [HttpPut("Usuario Coge Libro/{UsuarioId}")]
        public async Task<IActionResult> PutUsuarioCogeLibro(int UsuarioId, int LibroId)
        {
            return await _userServices.PutUsuarioCogeLibroService(UsuarioId, LibroId);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Modificar Usuario/{UsuarioId}")]
        public async Task<IActionResult> PutModificarUsuario(int UsuarioId, UsuarioBasico usu)
        {
            return await _userServices.PutModificarUsuarioService(UsuarioId, usu);
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Añadir Usuario/")]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioBasico usu)
        {
            return await _userServices.PostUsuarioServcice(usu);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("Borrar Usuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
           return await _userServices.DeleteUsuarioService(id);
        }

       
    }
}
