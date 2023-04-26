using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services.Auth
{
    public class AuthService
    {
        private static string _baseUrl;

        public AuthService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task Auth(Usuario usuario)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var user = new Usuario();
        }
    }
}
