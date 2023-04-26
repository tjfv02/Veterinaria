using Newtonsoft.Json;
using System.Text;
using VeterinariaMVC.Models;
using VeterinariaMVC.Models.Results;

namespace VeterinariaMVC.Services.Auth
{
    public class AuthService : IAuthService
    {
        private static string _baseUrl;
        private static string _token;

        public AuthService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task SignIn(Usuario usuario)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);

            var user = new Usuario()
            {
                Email = usuario.Email,
                Contraseña = usuario.Contraseña
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Auth/SignIn", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AuthResult>(jsonResponse);

            _token = result.Token;


        }

        public Task SignUp(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
