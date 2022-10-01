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

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly EntityDBContext _context;

        public UsuariosController(EntityDBContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
           
            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/LibrosDeUsuario/5
        //  Get the books of a user
        [HttpGet]
        [Route("api/LibrosDeUsuario/{UsuarioId}")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosDeUsuario(int UsuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(UsuarioId);
            var libros = await _context.Libros.ToListAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            var result = from lib in libros
                         where lib.UsuarioId == UsuarioId
                         select lib;

            return result.ToArray();
        }

        // PUT: api/UsuarioDevuelveLibro/
        [HttpPut("api/UsuarioDevuelveLibro/{UsuarioId}")]
        public async Task<IActionResult> PutUsuarioDevuelveLibro(int UsuarioId, int LibroId)
        {
            var usu = await _context.Usuarios.FindAsync(UsuarioId);
            var lib = await _context.Libros.FindAsync(LibroId);
            var usuAnt = await _context.UsuariosAntiguos.ToListAsync();

            if (usu == null || lib == null) return NotFound();

            if (UsuarioId != lib.UsuarioId) return BadRequest();

            lib.UsuarioId = null;

            _context.Entry(lib).State = EntityState.Modified;

            await _context.SaveChangesAsync();
                       
            UsuariosAntiguos ua = new UsuariosAntiguos()
            {
               
                LibroAntiguoId = lib.LibroId,
                UsuarioAntiguo = usu
                
            };

            _context.UsuariosAntiguos.Add(ua);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/UsuarioCogeLibro/
        [HttpPut("api/UsuarioCogeLibro/{UsuarioId}")]
        public async Task<IActionResult> PutUsuarioCogeLibro(int UsuarioId, int LibroId)
        {
            int usuLibId = 0;

            var usu = await _context.Usuarios.FindAsync(UsuarioId);
            var lib = await _context.Libros.FindAsync(LibroId);
            var usuAnt = await _context.UsuariosAntiguos.ToListAsync();

            if (usu == null || lib == null) return NotFound();

            if (lib.UsuarioId != null) usuLibId = (int)lib.UsuarioId;
           

            if(lib.UsuarioId != null) await PutUsuarioDevuelveLibro(usuLibId, LibroId);

            lib.UsuarioId = UsuarioId;

            _context.Entry(lib).State = EntityState.Modified;

            await _context.SaveChangesAsync();
                       
            return NoContent();
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("api/ModificarUsuario/{UsuarioId}")]
        public async Task<IActionResult> PutLibro(int UsuarioId, UsuarioBasico usu)
        {
            if (UsuarioId != usu.UsuarioId)
            {
                return BadRequest();
            }

            Usuario usuario = new Usuario()
            {
                UsuarioId = UsuarioId,
                Nombre = usu.Nombre
            };

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(UsuarioId))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioBasico usu)
        {
            Usuario usuario = new Usuario()
            {
                UsuarioId = usu.UsuarioId,
                Nombre = usu.Nombre,
                Libros = new List<Libro>()
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
