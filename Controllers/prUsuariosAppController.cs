using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NanniesAPI.AccesoDatos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static LoginAPP.Entidades.EntidadesNegocio;

namespace NanniesAPI.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class prUsuariosAppController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly AppSettings appSettings;

        public prUsuariosAppController(IOptions<AppSettings> appSettings)
        {
            httpClient = new HttpClient();
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> POST(int IdUsuarioApp, USUARIOS_APP pUsuarioApp)
        {
            try
            {
                string headerValue = Request.Headers["Z-Header"];

                string endpoint = string.Empty;
                if (headerValue == "0")
                {
                    endpoint = appSettings.Url + "prUsuariosApp/";
                }
                else if (headerValue == "1")
                {
                    endpoint = appSettings.Url1 + "prUsuariosApp/";
                }
                else
                {
                    return BadRequest("Invalid header value.");
                }

                string connectionString = headerValue == "0" ? Persistencia.strCon : Persistencia.strCon2;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Sis.prUSUARIOS_APP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdUsuarioApp", IdUsuarioApp);
                        command.Parameters.AddWithValue("@Correo", pUsuarioApp.Correo);
                        command.Parameters.AddWithValue("@Nombre", pUsuarioApp.Nombre);
                        command.Parameters.AddWithValue("@ApellidoPaterno", pUsuarioApp.ApellidoPaterno);
                        command.Parameters.AddWithValue("@ApellidoMaterno", pUsuarioApp.ApellidoMaterno);
                        command.Parameters.AddWithValue("@Contraseña", pUsuarioApp.Contraseña);
                        command.Parameters.AddWithValue("@Telefono", pUsuarioApp.Telefono);
                        command.Parameters.AddWithValue("@TelefonoAdicional", pUsuarioApp.TelefonoAdicional);
                        command.Parameters.AddWithValue("@Calle", pUsuarioApp.Calle);
                        command.Parameters.AddWithValue("@Numero", pUsuarioApp.Numero);
                        command.Parameters.AddWithValue("@Colonia", pUsuarioApp.Colonia);
                        command.Parameters.AddWithValue("@IdMunicipio", pUsuarioApp.IdMunicipio);
                        command.Parameters.AddWithValue("@Activo", pUsuarioApp.Activo);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                int result = (int)reader["Result"];
                                string mensaje = (string)reader["Mensaje"];
                                int ident = (int)reader["Ident"];

                                Response apiResponse = new Response
                                {
                                    Result = result,
                                    Mensaje = mensaje,
                                    Ident = ident
                                };

                                return Ok(apiResponse);
                            }
                        }
                    }
                }

                return StatusCode(500, new { Mensaje = "Error executing stored procedure.", result = 1 });
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new { Mensaje = Ex.Message, result = 1 });
            }
        }

    }
}
