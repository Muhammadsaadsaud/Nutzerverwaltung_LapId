using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// [UpdateUserDto] Eine DTO zum Bearbeiten eines Nutzers
    /// </summary>
    public class UpdateUserDto
    {

        // Vorname eines Benutzers
        [Required]
        public string Firstname { get; set; }

        // Nachname eines Benutzers
        [Required]
        public string Lastname { get; set; }

        // Login name eines Benutzers
        [Required]
        public string Login { get; set; }

        // Passwort eines Benutzers
        public string Password { get; set; }

    }
}
