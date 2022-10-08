using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Helpers;
using Example_of_Entityframework_Core.Models.DataModels;
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

        public AccountController(EntityDBContext context, JwtSettings jwtSettings, IUsuarioServices usuarioServices)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _usuarioServices = usuarioServices;
        }

        [HttpPost("Login/")]
        public IActionResult GetToken(UserLogins userLogin)
        {

            try
            {
                var Token = new UserTokens();

                var searchUser = (from user in _context.GrantedUsers
                                  where user.Email == userLogin.Email && user.Password == userLogin.Password
                                  select user).FirstOrDefault();





               if (searchUser != null)
                {
                    
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.GrantedUserId,
                        GuidId = Guid.NewGuid(),
                        Role = searchUser.Role

                    }, _jwtSettings
                    );
                   
                }
                else
                {
                    return BadRequest("Wrong Credentials");
                }
                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);

            }

        }

        //Create GrantedUser to interact with DB
        [HttpPost("Solo Admin/Crear UsuarioDB/")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<GrantedUser>>> CreateGrantedUser(GrantedUser gu)
        {
            GrantedUser usuario = new GrantedUser()
            {
                Name = gu.Name,
                LastName = gu.LastName,
                Email = gu.Email,
                Password = gu.Password,
                Role = gu.Role
            };

            _context.GrantedUsers.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario creado con éxito");
        }

    }

}
