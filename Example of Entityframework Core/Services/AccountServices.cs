using Example_of_Entityframework_Core.DataAccess;
using Example_of_Entityframework_Core.Helpers;
using Example_of_Entityframework_Core.Models.DataModels;
using Example_of_Entityframework_Core.Models.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Example_of_Entityframework_Core.Services
{
    public class AccountServices : ControllerBase, IAccountServices
    {
        private EntityDBContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioServices _usuarioServices;

        public AccountServices(EntityDBContext context, JwtSettings jwtSettings, IUsuarioServices usuarioServices)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _usuarioServices = usuarioServices;
        }

        public async Task<IActionResult> GetTokenService(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var searchUser = await (from user in _context.GrantedUsers
                                        where user.Email == userLogin.Email && user.Password == userLogin.Password
                                        select user).FirstOrDefaultAsync();

                if (searchUser != null && searchUser.isActive)
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
                else if (searchUser != null && !searchUser.isActive)
                {
                    return BadRequest("Usuario dado de baja. Avisa al administrador");
                }
                else
                {
                    return BadRequest("Credenciales incorrectas. Vuelva a introducir sus datos.");
                }
                Object[] result = new Object[]
                    {
                        $"Bienvenido {Token.UserName}!",
                        $"Rol: {Token.Role}. Tiene accesibles las operaciones de dicho rol.",
                        $"Token: {Token.Token}"
                    };
                return Ok(result);

            }
            catch (Exception ex)
            {
                throw new Exception("Error en GetToken", ex);

            }
        }

        public async Task<ActionResult> PostGrantedUserService(GrantedUser gu)
        {
            GrantedUser usuario = new GrantedUser()
            {
                Name = gu.Name,
                LastName = gu.LastName,
                Email = gu.Email,
                Password = gu.Password,
                Role = gu.Role
            };

            try
            {
                _context.GrantedUsers.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuario creado con éxito");
            }
            catch
            {
                return BadRequest("Error al crear el usuario.");
            }

        }

        public async Task<ActionResult> GetGrantedUsersService()
        {
            var grantedUsers = await _context.GrantedUsers.ToListAsync();
            return Ok(grantedUsers);
        }

        public async Task<IActionResult> PutModificarGrantedUserService(int guId, GrantedUser gu)
        {
            if (guId != gu.GrantedUserId)
            {
                return BadRequest("Pon el mismo GrantedUserId en los dos sitios.");
            }

            GrantedUser usuario = new GrantedUser()
            {
                GrantedUserId = gu.GrantedUserId,
                Name = gu.Name,
                LastName = gu.LastName,
                Email = gu.Email,
                Password = gu.Password,
                Role = gu.Role,
                isActive = gu.isActive
            };

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrantedUserExists(guId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Usuario modificado con éxito.");
        }

        public async Task<IActionResult> PutBajaGrantedUserService(int guId)
        {
            GrantedUser gu = await _context.GrantedUsers.FindAsync(guId);

            gu.isActive = false;
            
            _context.Entry(gu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrantedUserExists(guId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Usuario dado de baja con éxito.");
        }

        public async Task<IActionResult> PutAltaGrantedUserService(int guId)
        {
            GrantedUser gu = await _context.GrantedUsers.FindAsync(guId);

            gu.isActive = true;

            _context.Entry(gu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrantedUserExists(guId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Usuario dado de alta con éxito.");
        }

        private bool GrantedUserExists(int id)
        {
            return _context.GrantedUsers.Any(e => e.GrantedUserId == id);
        }
    }
}
