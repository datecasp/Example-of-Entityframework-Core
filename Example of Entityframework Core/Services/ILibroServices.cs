using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;

namespace Example_of_Entityframework_Core.Services
{
    public interface ILibroServices
    {
        Task<IEnumerable<Libro>> GetLibrosService();
        Task<ActionResult<CategoriasDeLibro>> GetCategoriasDeLibroService(int LibroId);
        Task<ActionResult<Libro>> GetLibroPorIdService(int id);
        Task<ActionResult<ICollection<UsuarioBasico>>> GetUsuariosDeLibroService(int id);
        Task<IActionResult> PutModificarLibroService(int id, LibroBasico lib);
        Task<IActionResult> PutLibroEnCategoriasService(int LibroId, int CategoriaId);
        Task<ActionResult<LibroBasico>> PostLibroService(LibroBasico libro);
        Task<IActionResult> DeleteLibroService(int id);
    }
}
