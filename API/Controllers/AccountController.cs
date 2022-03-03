using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Kontobezogene Anfragen (Register, Login, Update, Delete)
    /// </summary>
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instanz von DataContext,
        /// als Dependency Injection übergeben</param>
        public AccountController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Erstellen eines Nutzers
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>UserDto</returns>
        [HttpPost("users")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // ob ein Login vorhanden ist
            if (await UserExists(registerDto.Login))
            {
                // gib 400 Badrequest zurück, wenn Login existiert
                return BadRequest("Login is Taken");
            }
            // eine Instanz von HMACSHA512 erstellt
            using var hmac = new HMACSHA512();

            var user = new User();
            user.Firstname = registerDto.Firstname.ToLower();
            user.Lastname = registerDto.Lastname.ToLower();
            user.Login = registerDto.Login.ToLower();
            user.CreationDate = DateTime.Now;
            user.ChangeDate = DateTime.Now;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            // Hinzufügen eines Benutzers zur Datenbank
            _context.Users.Add(user);
            // Änderungen asynchron in der Datenbank speichern
            await _context.SaveChangesAsync();
            // [User] in ein [UserDto] umwandeln
            var userDto = ConvertToUserDto(user);
            // Erfolgreiche Erstellung gibt einen Status Code 201 mit [UserDto] zurück
            return Created($"~api/Users/{user.Id}", userDto);
        }

        /// <summary>
        /// Bearbeiten eines Nutzers
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <param name="id">ID eines Benutzers</param>
        /// <returns>[UserDto]</returns>
        [HttpPut("users/{id}")]
        public async Task<ActionResult<UserDto>> Update(UpdateUserDto updateUserDto, int id)
        {
            var user = await _context.Users.FindAsync(id);

            // ob ein User vorhanden ist
            if (user == null)
            {
                // gib 400 Badrequest zurück, wenn User nicht existiert
                return BadRequest("Invalid User");
            }

            // ob ein Login vorhanden ist
            if (await UserExists(updateUserDto.Login))
            {
                // gib 400 Badrequest zurück, wenn Login existiert
                return BadRequest("Login is Taken");
            }
            using var hmac = new HMACSHA512();

            user.Firstname = updateUserDto.Firstname.ToLower();
            user.Lastname = updateUserDto.Lastname.ToLower();
            user.Login = updateUserDto.Login.ToLower();
            user.ChangeDate = DateTime.Now;
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updateUserDto.Password));
            }
            // Änderungen asynchron in der Datenbank speichern
            await _context.SaveChangesAsync();

            var userDto = ConvertToUserDto(user);
            // Erfolgreiche Erstellung gibt einen Status Code 200 mit [UserDto] zurück
            return Ok(userDto);
        }

        /// <summary>
        /// Eine private Methode, um zu überprüfen,
        /// ob ein Benutzer in einer Datenbank vorhanden ist
        /// </summary>
        /// <param name="login"></param>
        /// <returns>bool(true/false)</returns>
        private async Task<bool> UserExists(string login)
        {
            return await _context.Users.AnyAsync(x => x.Login == login.ToLower());
        }

        /// <summary>
        /// [User] in ein [UserDto] umwandeln
        /// </summary>
        /// <param name="user"></param>
        /// <returns>[UserDto]</returns>
        private UserDto ConvertToUserDto(User user)
        {
            var userDto = new UserDto();
            userDto.Id = user.Id;
            userDto.Firstname = user.Firstname;
            userDto.Lastname = user.Lastname;
            userDto.CreationDate = user.CreationDate;
            userDto.ChangeDate = user.ChangeDate;
            userDto.Login = user.Login;
            return userDto;
        }


    }
}
