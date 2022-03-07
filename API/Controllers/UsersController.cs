using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Benutzerbezogene Anfragen
    /// </summary>
    public class UsersController : BaseApiController
    {
        private readonly BLL.UserBLL _BLL;
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController()
        {
            _BLL = new BLL.UserBLL();
        }

        /// <summary>
        /// Auflisten von allen Nutzern
        /// </summary>
        /// <returns>[UserDto]</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            // Alle Benutzer aus der Datenbank in einer Liste erhalten
            var usersList = await _BLL.GetUsers();
            // Erfolgreiche Erstellung gibt einen Status Code 200 mit [List<UserDto>] zurück
            return Ok(usersList);

        }

        /// <summary>
        /// Gibt einen Benutzer mit einer bestimmten ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns>[User]</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            return await _BLL.GetUser(id);
        }

    }
}
