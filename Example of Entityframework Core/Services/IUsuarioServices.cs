using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;

namespace Example_of_Entityframework_Core.Services
{
    public interface IUsuarioServices
    {
        Task<IEnumerable<Usuario>> GetUsersService();
        Task<ActionResult<Usuario>> GetUsuarioPorIdService(int id);
        Task<ActionResult<IEnumerable<Libro>>> GetLibrosDeUsuarioService(int UsuarioId);
        Task<IActionResult> PutUsuarioDevuelveLibroService(int UsuarioId, int LibroId);
        Task<IActionResult> PutUsuarioCogeLibroService(int UsuarioId, int LibroId);
        Task<IActionResult> PutModificarUsuarioService(int UsuarioId, UsuarioBasico usu);
        Task<ActionResult<Usuario>> PostUsuarioServcice(UsuarioBasico usu);
        Task<IActionResult> DeleteUsuarioService(int id);
    }
}
