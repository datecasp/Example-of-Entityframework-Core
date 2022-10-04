using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;

namespace Example_of_Entityframework_Core.Services
{
    public interface ICategoriaServices
    {
        Task<IEnumerable<Categorias>> GetCategoriasService();
        Task<ActionResult<LibrosPorCategoria>> GetLibrosPorCategoriaService(int categoriaId);
        Task<ActionResult<Categorias>> GetCategoriaPorIdService(int id);
        Task<IActionResult> PutModificarCategoriaService(int catId, CategoriaBasica cat);
        Task<IActionResult> PutLibroEnCategoriasService(int CategoriaId, int LibroId);
        Task<ActionResult<Categorias>> PostCategoriasService(CategoriaBasica cat);
        Task<IActionResult> DeleteCategoriasService(int id);
    }
}
