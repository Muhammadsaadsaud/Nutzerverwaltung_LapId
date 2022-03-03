
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Benutzerbezogene Anfragen
    /// </summary>
    public class UsersController : BaseApiController
    {

        private readonly DataContext _context;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">
        /// Instanz von DataContext,
        /// als Dependency Injection übergeben</param>
        public UsersController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Auflisten von allen Nutzern
        /// </summary>
        /// <returns>[UserDto]</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            // Alle Benutzer aus der Datenbank in einer Liste erhalten
            var usersList = await _context.Users.ToListAsync();
            // Eine leere Ilist vom Typ [UserDto] erstellt
            IList<UserDto> users = new List<UserDto>();
            // Den [User] in [UserDto] konvertiert
            // und fügt ihn in der Liste hinzu
            foreach (var user in usersList)
            {
                users.Add(ConvertToUserDto(user));
            }
            // Erfolgreiche Erstellung gibt einen Status Code 200 mit [List<UserDto>] zurück
            return Ok(users);

        }

        /// <summary>
        /// Gibt einen Benutzer mit einer bestimmten ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns>[User]</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
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
