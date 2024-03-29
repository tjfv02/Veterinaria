﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using VeterinariaMVC.Models;
using VeterinariaMVC.Services.Auth;

namespace VeterinariaMVC.Services
{
    public class CitaService : ICitaService
    {
        private static string _baseUrl;
        private static string _token;


        public static int timeout = 30;
        public CitaService(IAuthService authService)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;

            _token = authService.Token;

        }
        public async Task<bool> Delete(int citaId)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.DeleteAsync("Citas/" + citaId);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Edit(Cita cita)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var content = new StringContent(JsonConvert.SerializeObject(cita), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("Citas", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<Cita> Get(int citaId)
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.GetAsync("Citas/" + citaId);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Cita>(jsonResult);
                Cita cita = result;
                return cita;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<List<Cita>> List()
        {
            List<Cita> List = new List<Cita>();

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await client.GetAsync("Citas");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Cita>>(jsonResult);
                List = result;
            }

            return List;
        }

        public async Task<bool> Save(Cita cita)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var content = new StringContent(JsonConvert.SerializeObject(cita), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Citas", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}
