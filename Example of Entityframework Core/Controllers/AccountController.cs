using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Helpers;
using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Example_of_Entityframework_Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Example_of_Entityframework_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EntityDBContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IAccountServices _accountServices;

        public AccountController(EntityDBContext context, JwtSettings jwtSettings, IUsuarioServices usuarioServices, IAccountServices accountServices)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _usuarioServices = usuarioServices;
            _accountServices = accountServices;
        }

        [HttpPost("Login/")]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            return await _accountServices.GetTokenService(userLogin);
            

        }

        //Create GrantedUser to interact with DB
        [HttpPost("Solo Admin/Crear UsuarioDB/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> PostGrantedUser(GrantedUser gu)
        {
            return await _accountServices.PostGrantedUserService(gu);
        }

        //Retrieve GrantedUsers of DB
        [HttpGet("Solo Admin/Ver UsuariosDB/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> GetGrantedUsers()
        {
            return await _accountServices.GetGrantedUsersService();
        }

        //Modify GrantedUser of DB
        [HttpPut("Solo Admin/Modificar UsuarioDB/{GrantedUserId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutModificarGrantedUser(int GrantedUserId, GrantedUser gu)
        {
            return await _accountServices.PutModificarGrantedUserService(GrantedUserId, gu);
        }

        //Modify GrantedUser of DB isActive flag to false
        [HttpPut("Solo Admin/Baja UsuarioDB/{GrantedUserId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutBajaGrantedUser(int GrantedUserId)
        {
            return await _accountServices.PutBajaGrantedUserService(GrantedUserId);
        }

        //Modify GrantedUser of DB isActive flag to true
        [HttpPut("Solo Admin/Alta UsuarioDB/{GrantedUserId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutAltaGrantedUser(int GrantedUserId)
        {
            return await _accountServices.PutAltaGrantedUserService(GrantedUserId);
        }
    }

}
