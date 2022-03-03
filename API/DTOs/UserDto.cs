namespace API.DTOs
{
    /// <summary>
    /// [UserDto] Ein DTO zum Auflisten aller Benutzer
    /// </summary>
    public class UserDto
    {
        // ID eines Benutzers
        public int Id { get; set; }

        // Vorname eines Benutzers
        public string Firstname { get; set; }

        // Nachname eines Benutzers
        public string Lastname { get; set; }

        // Login name eines Benutzers
        public string Login { get; set; }

        // Datum an dem der Nutzer erstellt wurde
        public DateTime CreationDate { get; set; }

        // Datum an dem der Nutzer das letzte Mal geändert worden ist
        public DateTime ChangeDate { get; set; }

    }
}
