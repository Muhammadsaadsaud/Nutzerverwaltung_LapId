namespace API.Entities
{
    /// <summary>
    /// Eigenschaften eines Nutzers
    /// </summary>
    public class User
    {
        // ID eines Benutzers
        public int Id { get; set; }

        // Vorname eines Benutzers
        public string Firstname { get; set; }

        // Nachname eines Benutzers
        public string Lastname { get; set; }

        // Login name eines Benutzers
        public string Login { get; set; }

        // PasswortHash eines Benutzers
        public byte[] PasswordHash { get; set; }

        // PasswortSalt eines Benutzers
        public byte[] PasswordSalt { get; set; }

        // Datum an dem der Nutzer erstellt wurde
        public DateTime CreationDate { get; set; }
      
        // Datum an dem der Nutzer das letzte Mal geändert worden ist
        public DateTime ChangeDate { get; set; }
    }
}
