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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return await _userServices.GetUsersService();
        }

        // GET: api/Usuarios/5
        [HttpGet("Usuario Por Id/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            return await _userServices.GetUsuarioPorIdService(id);
        }

        // GET: api/LibrosDeUsuario/5
        //  Get the books of a user
        [HttpGet]
        [Route("Libros De Usuario/{UsuarioId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosDeUsuario(int UsuarioId)
        {
            return await _userServices.GetLibrosDeUsuarioService(UsuarioId);
        }

        // PUT: api/UsuarioDevuelveLibro/
        [HttpPut("Usuario Devuelve Libro/{UsuarioId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> PutUsuarioDevuelveLibro(int UsuarioId, int LibroId)
        {
            return await _userServices.PutUsuarioDevuelveLibroService(UsuarioId, LibroId);
        }

        // PUT: api/UsuarioCogeLibro/
        [HttpPut("Usuario Coge Libro/{UsuarioId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> PutUsuarioCogeLibro(int UsuarioId, int LibroId)
        {
            return await _userServices.PutUsuarioCogeLibroService(UsuarioId, LibroId);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Solo Admin/Modificar Usuario/{UsuarioId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutModificarUsuario(int UsuarioId, UsuarioBasico usu)
        {
            return await _userServices.PutModificarUsuarioService(UsuarioId, usu);
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Solo Admin/Añadir Usuario/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioBasico usu)
        {
            return await _userServices.PostUsuarioServcice(usu);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("Solo Admin/Borrar Usuario/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
           return await _userServices.DeleteUsuarioService(id);
        }

       
    }
}
