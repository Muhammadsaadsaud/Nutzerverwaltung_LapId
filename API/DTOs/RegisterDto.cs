using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    /// <summary>
    /// [RegisterDto] für die Benutzerregistrierung und -erstellung verwendet.
    /// Alle Felder sind Pflichtfelder (Firstname, Lastname, Login & Password)
    /// </summary>
    public class RegisterDto
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
        [Required]
        public string Password { get; set; }

    }
}
