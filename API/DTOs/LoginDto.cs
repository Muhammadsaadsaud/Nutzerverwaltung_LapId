namespace API.DTOs
{
    /// <summary>
    /// [LoginDto] für Login-Endpunkt verwendet und hat nur Login und Passwort
    /// </summary>
    public class LoginDto
    {
        // Login name eines Benutzers
        public string Login { get; set; }

        // PasswortHash eines Benutzers
        public string Password { get; set; }
    }
}
