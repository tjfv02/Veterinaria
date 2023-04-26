using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using VeterinariaMVC.Models;
using VeterinariaMVC.Services.Auth;

namespace VeterinariaMVC.Services
{
    public class VeterinarioService: IVeterinarioService
    {
        private static string _baseUrl;
        private static string _token;

        public static int timeout = 30;

        public VeterinarioService(IAuthService authService)
        {
            //Obtener valores del App Settings
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;

            _token = authService.Token;

        }

        // Veterinarios
        public async Task<List<Veterinario>> List()
        {
            List<Veterinario> List = new List<Veterinario>();

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.GetAsync("Veterinarios");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Veterinario>>(jsonResult);
                List = result;
            }

            return List;

        }

        public async Task<Veterinario> Get(int veterinarioId)
        {

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.GetAsync("Veterinarios/" + veterinarioId);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Veterinario>(jsonResult);
                Veterinario Veterinario = result;
                return Veterinario;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<bool> Save(Veterinario veterinario)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var content = new StringContent(JsonConvert.SerializeObject(veterinario), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Veterinarios", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Edit(Veterinario veterinario)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var content = new StringContent(JsonConvert.SerializeObject(veterinario), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Veterinarios", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Delete(int veterinarioId)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.DeleteAsync("Veterinarios/" + veterinarioId);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
