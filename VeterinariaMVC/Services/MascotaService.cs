using Newtonsoft.Json;
using System.Text;
using VeterinariaMVC.Models;
using VeterinariaMVC.Models.Results;

namespace VeterinariaMVC.Services
{
    public class MascotaService : IMascotaService
    {
        private static string _baseUrl;

        public static int timeout = 30;

        public MascotaService()
        {
            //Obtener valores del App Settings
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        //Autenticación futura
        public async Task Auth()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
        }

        // Mascotas
        public async Task<List<Mascota>> List()
        {
            List<Mascota> List= new List<Mascota>();

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Mascotas");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Mascota>>(jsonResult);
                List = result;
            }

            return List;

        }

        public async Task<Mascota> Get(int mascotaId)
        {

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.GetAsync("Mascotas/" + mascotaId);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Mascota>(jsonResult);
                Mascota mascota = result;
                return mascota;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<bool> Save(Mascota mascota)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(mascota), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Mascotas", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Edit(Mascota mascota)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(mascota), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Mascotas", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Delete(int mascotaId)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);

            var response = await client.DeleteAsync("Mascotas/" + mascotaId);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
