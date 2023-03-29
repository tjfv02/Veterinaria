using Newtonsoft.Json;
using System.Text;
using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public class UsuarioService : IUsuarioService
    {
        private static string _baseUrl;

        public static int timeout = 30;

        public UsuarioService()
        {
            //Obtener valores del App Settings
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }


        public async Task<List<Usuario>> List()
        {
            List<Usuario> List = new List<Usuario>();

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Usuarios");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Usuario>>(jsonResult);
                List = result;
            }

            return List;

        }

        public async Task<Usuario> Get(int usuarioId)
        {

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Usuarios/" + usuarioId);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Usuario>(jsonResult);
                Usuario Usuario = result;
                return Usuario;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<bool> Save(Usuario usuario)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Usuarios", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Edit(Usuario usuario)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Usuarios", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Delete(int usuarioId)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.DeleteAsync("Usuarios/" + usuarioId);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
