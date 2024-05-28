using MvcPeliculasApiAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcPeliculasApiAWS.Services
{
    public class ServicePeliculas
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue header;
        private IHttpContextAccessor context;

        public ServicePeliculas(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("APIsUrls:ApiPeliculas");
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");


        }

        #region CALLAPISASYNC
        // sin token
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(this.ApiUrl + request);

                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        #endregion

        #region PELICULAS

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/peliculas";
            List<Pelicula> data = await this.CallApiAsync<List<Pelicula>>(request);
            return data;
        }

        public async Task<List<Pelicula>> GetPeliculasByActorAsync(string actor)
        {
            string request = "api/Peliculas/PeliculasActor/" + actor;
            List<Pelicula> data = await this.CallApiAsync<List<Pelicula>>(request);
            return data;
        }

        public async Task<Pelicula> GetPeliculaByIdAsync(int id)
        {
            string request = "api/peliculas/" + id;
            Pelicula pelicula = await this.CallApiAsync<Pelicula>(request);
            return pelicula;
        }

        public async Task<Pelicula> InsertarPeliculaAsync(Pelicula peli)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                string request = "api/peliculas";
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(peli);
                StringContent stringContent = new StringContent(json, this.header);

                HttpResponseMessage response =
                    await httpClient.PostAsync(this.ApiUrl + request, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    Pelicula pelicula = await response.Content.ReadAsAsync<Pelicula>();
                    return pelicula;
                }
                else
                {
                    return null;
                }
            }
        }


        public async Task<Pelicula> UpdatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string request = "api/peliculas";
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(pelicula);
                StringContent stringContent = new StringContent(json, this.header);

                HttpResponseMessage response =
                    await httpClient.PutAsync(this.ApiUrl + request, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    Pelicula peli = await response.Content.ReadAsAsync<Pelicula>();
                    return pelicula;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task DeletePeliculaAsync(int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string request = "api/peliculas/" + id;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response =
                    await httpClient.DeleteAsync(this.ApiUrl + request);
            }
        }

        #endregion
    }
}
