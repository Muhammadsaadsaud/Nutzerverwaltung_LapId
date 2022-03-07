using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Kontobezogene Anfragen (Register, Login, Update, Delete)
    /// </summary>
    public class AccountController : BaseApiController
    {
        private readonly BLL.UserBLL _BLL;
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountController()
        {
            _BLL = new BLL.UserBLL();
        }

        /// <summary>
        /// Erstellen eines Nutzers
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>UserDto</returns>
        [HttpPost("users")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await _BLL.CreateUser(registerDto);

            if (userDto == null)
            {
                // gib 400 Badrequest zurück, wenn etwas schief gelaufen ist
                return BadRequest("Invalid login");
            }
            else
            {
                // Erfolgreiche Erstellung gibt einen Status Code 201 mit [UserDto] zurück
                return Created($"~api/Users/{userDto.Id}", userDto);
            }
        }

        /// <summary>
        /// Bearbeiten eines Nutzers
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <param name="id">ID eines Benutzers</param>
        /// <returns>[UserDto]</returns>
        [HttpPut("users/{id}")]
        public async Task<ActionResult<bool>> Update(UpdateUserDto updateUserDto, int id)
        {
            // ob ein User vorhanden ist, dann aktualisieren und true zurückgeben
            if (!await _BLL.UpdateUser(id, updateUserDto))
            {
                // gib 400 Badrequest zurück, wenn User nicht existiert
                return BadRequest("Login name is already taken");
            }
            else
            {

                // Erfolgreiche Bearbeitung gibt einen Status Code 200 zurück
                return Ok("User has been successfully updated");

            }
        }

        /// <summary>
        /// Löschen eines Nutzers
        /// </summary>
        /// <param name="id">ID eines Benutzers</param>
        [HttpDelete("users/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // ob ein User vorhanden ist
            if (!await _BLL.DeleteUser(id))
            {
                // Eine fehlerhafte Löschung wird mit Status Code 400 angezeigt
                return BadRequest("Invalid User");
            }
            else
            {
                // Die erfolgreiche Löschung gibt einen Status Code 200 zurück
                return Ok("User has been deleted successfully");
            }
        }

        /// <summary>
        /// Anmelden als Nutzer
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>[UserDto]</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await _BLL.LoginUser(loginDto);
            // ob ein Loign vorhanden ist
            if (userDto == null)
            {
                // Sind Login oder Passwort falsch soll dies einen Status Code 400 erzeugen
                return BadRequest("Invalid Login");
            }
            // Sind Login und Passwort richtig soll dies einen Status Code 200 erzeugen
            return Ok(userDto);
        }
    }
}
