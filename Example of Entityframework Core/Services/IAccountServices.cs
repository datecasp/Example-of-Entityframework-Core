using Example_of_Entityframework_Core.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace Example_of_Entityframework_Core.Services
{
    public interface IAccountServices
    {
        Task<IActionResult> GetTokenService(UserLogins userLogin);
        Task<ActionResult> PostGrantedUserService(GrantedUser gu);
        Task<ActionResult> GetGrantedUsersService();
        Task<IActionResult> PutModificarGrantedUserService(int guId, GrantedUser gu);
        Task<IActionResult> PutBajaGrantedUserService(int guId);
        Task<IActionResult> PutAltaGrantedUserService(int guId);
    }
}
