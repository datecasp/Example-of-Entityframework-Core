using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example_of_Entityframework_Core.Services
{
    public class UsuarioServices : ControllerBase, IUsuarioServices
    {
        private EntityDBContext _context;

        public UsuarioServices(EntityDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetUsersService()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return usuarios;
        }

        public async Task<ActionResult<Usuario>> GetUsuarioPorIdService(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosDeUsuarioService(int UsuarioId)
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

        public async Task<IActionResult> PutUsuarioDevuelveLibroService(int UsuarioId, int LibroId)
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

        public async Task<IActionResult> PutUsuarioCogeLibroService(int UsuarioId, int LibroId)
        {
            int usuLibId = 0;

            var usu = await _context.Usuarios.FindAsync(UsuarioId);
            var lib = await _context.Libros.FindAsync(LibroId);
            var usuAnt = await _context.UsuariosAntiguos.ToListAsync();

            if (usu == null || lib == null) return NotFound();

            if (lib.UsuarioId != null) { 
                usuLibId = (int)lib.UsuarioId; 
                await PutUsuarioDevuelveLibroService(usuLibId, LibroId); }

            lib.UsuarioId = UsuarioId;

            _context.Entry(lib).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<IActionResult> PutModificarUsuarioService(int UsuarioId, UsuarioBasico usu)
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

        public async Task<ActionResult<Usuario>> PostUsuarioServcice(UsuarioBasico usu)
        {
            Usuario usuario = new Usuario()
            {
                UsuarioId = usu.UsuarioId,
                Nombre = usu.Nombre,
                Libros = new List<Libro>()
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<IActionResult> DeleteUsuarioService(int id)
        {
            /***********************************
             * 
             *      Implementar dar de baja a usuario
             *      
             *      en lugar de borrarlo, creando un
             *      
             *      campo nuevo fecha de baja.
             *      
             *      Si el campo es NULL, está activo
             *      
             *      si no, está de baja y NO DEBERÏA
             *      
             *      poder coger libros (flag)
             * 
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
             ********************/
            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
           
    }
}
