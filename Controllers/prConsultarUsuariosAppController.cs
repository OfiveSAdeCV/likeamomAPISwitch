using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NanniesAPI.AccesoDatos;
using static LoginAPP.Entidades.EntidadesNegocio;
using System.Net.Http;
using Newtonsoft.Json;
using LoginAPP.AccesoDatos;
using Microsoft.Extensions.Options;

namespace NanniesAPI
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class prConsultarUsuarioAppController : ControllerBase
    {

        private readonly HttpClient httpClient;
        private readonly AppSettings appSettings;

        public prConsultarUsuarioAppController(IOptions<AppSettings> appSettings)
        {
            httpClient = new HttpClient();
            this.appSettings = appSettings.Value;
        }

        [HttpGet("{pIdUsuarioApp}")]
        public async Task<USUARIOS_APP> prConsultarUsuarioApp(int pIdUsuarioApp)
        {
            string apiUrl;
            string headerValue = Request.Headers["X-Header"];

            if (headerValue == "0")
            {
                apiUrl = appSettings.Url + "prConsultarUsuarioApp/";
            }
            else if (headerValue == "1")
            {
                apiUrl = appSettings.Url1 + "prConsultarUsuarioApp/";
            }
            else
            {
                throw new ArgumentException("Invalid X-Header value.");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("X-Header", headerValue);

                var response = await client.GetAsync(pIdUsuarioApp.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<USUARIOS_APP>(data);
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}