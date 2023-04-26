using Newtonsoft.Json;
using System.Text;
using System.Threading;
using VeterinariaMVC.Models;
using VeterinariaMVC.Models.Results;

namespace VeterinariaMVC.Services.Auth
{

    public class AuthService : IAuthService
    {
        private static string _baseUrl;
        private static string _token;
        private static string _mensaje;
        private static bool _auth;

        public static int timeout = 30;

        public string Token
        {
            get { return _token; }
        }
        public string Mensaje
        {
            get { return _mensaje; }
        }
        public bool Auth
        {
            get { return _auth; }
        }

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
            _auth = result.Auth;
            _mensaje = result.Mensaje;

        }

        public async Task SignUp(Usuario usuario)
        {

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Auth/SignUp", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthResult>(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                _auth = result.Auth;
                _mensaje = result.Mensaje;
            }


        }
    }
}
