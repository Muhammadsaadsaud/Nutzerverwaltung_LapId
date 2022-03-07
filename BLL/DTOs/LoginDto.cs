namespace BLL.DTOs
{
    /// <summary>
    /// [LoginDto] für Login-Endpunkt verwendet und hat nur Login und Passwort
    /// </summary>
    public class LoginDto
    {
        // Login name eines Benutzers
        public string Login { get; set; }

        // Passwort eines Benutzers
        public string Password { get; set; }
    }
}
