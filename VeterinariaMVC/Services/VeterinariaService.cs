using Newtonsoft.Json;
using System.Text;
using VeterinariaMVC.Models;

namespace VeterinariaMVC.Services
{
    public class VeterinariaService: IVeterinariaService
    {
        private static string _baseUrl;
        public static int timeout = 30;

        public VeterinariaService()
        {
            //Obtener valores del App Settings
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        // Veterinarias
        public async Task<List<Veterinaria>> List()
        {
            List<Veterinaria> List = new List<Veterinaria>();

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Veterinarias");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Veterinaria>>(jsonResult);
                List = result;
            }

            return List;

        }

        public async Task<Veterinaria> Get(int veterinariaId)
        {

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Veterinarias/" + veterinariaId);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Veterinaria>(jsonResult);
                Veterinaria veterinaria = result;
                return veterinaria;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<bool> Save(Veterinaria veterinaria)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(veterinaria), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Veterinarias", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Edit(Veterinaria veterinaria)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(veterinaria), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Veterinarias", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Delete(int veterinariaId)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.DeleteAsync("Veterinarias/" + veterinariaId);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
