namespace VeterinariaMVC.Models.Results
{
    public class AuthResult
    {
        public string Mensaje { get; set; }
        public bool Auth { get; set; }
        public string? Token { get; set; }
    }
}
