using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using NanniesAPI.AccesoDatos;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static LoginAPP.Entidades.EntidadesNegocio;

namespace LoginAPP.AccesoDatos
{
    public class ad
    {
        private string headerValue;
    

        public ad(string headerValue)
        {
            this.headerValue = headerValue;

        }

      
        public static DataTable ConvertToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i <= propertyDescriptorCollection.Count - 1; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count - 1 + 1];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static ResponseLogIn LoginInApp(LogInAppCredentials pLoginCredentials)
        {
            using (SqlConnection sqlcon = new SqlConnection(Persistencia.strCon))
            {
                return sqlcon.QuerySingle<ResponseLogIn>("Sis.prValidaLoginApp1", pLoginCredentials, commandType: CommandType.StoredProcedure);
            }
        }

        public static DatosContacto prConsultarDatosContacto()
        {
            using SqlConnection sqlcon = new SqlConnection(Persistencia.strCon);
            return sqlcon.QuerySingle<DatosContacto>("[Ent].[prConsultarDatosContacto]");
        }

        public static int prFCM_TOKENS(TOKEN_UPDATE token)
        {
            using SqlConnection sqlcon = new SqlConnection(Persistencia.strCon);
            var values = new
            {
                token.IdUsuarioApp,
                token.Token,
                token.DeviceID
            };

            return sqlcon.Execute("Sis.prFCM_TOKENS", values, commandType: System.Data.CommandType.StoredProcedure);

            //return sqlcon.QuerySingle<Response>("Sis.prFCM_TOKENS", values, commandType: System.Data.CommandType.StoredProcedure);
        }

        public static List<ELEMENTOS_CARRUSEL> prConsultarElementosCarrusel()
        {
            using (SqlConnection sqlcon = new SqlConnection(Persistencia.strCon))
            {

                return sqlcon.Query<ELEMENTOS_CARRUSEL>($"Cat.prConsultarElementosCarrusel").ToList();
            }
        }


     /*   public async Task<USUARIOS_APP> prConsultarUsuarioApp(int pIdUsuarioApp)
        {
            string apiUrl;

            if (headerValue == "0")
            {
                apiUrl = "http://192.168.0.79/LikeamomAPI/v1/prConsultarUsuarioApp/";
            }
            else if (headerValue == "1")
            {
                apiUrl = "http://192.168.0.79/LikeamomAPISuc01/v1/prConsultarUsuarioApp/";
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
        }*/



      /*  public static Response prUSUARIOS_APP(int IdUsuarioApp, USUARIOS_APP pUsuarioApp)
        {
            using (SqlConnection sqlcon = new SqlConnection(Persistencia.strCon))
            {
                var values = new
                {
                    IdUsuarioApp,
                    pUsuarioApp.Correo,
                    pUsuarioApp.Nombre,
                    pUsuarioApp.ApellidoPaterno,
                    pUsuarioApp.ApellidoMaterno,
                    pUsuarioApp.Contraseña,
                    pUsuarioApp.Telefono,
                    pUsuarioApp.TelefonoAdicional,
                    pUsuarioApp.Calle,
                    pUsuarioApp.Numero,
                    pUsuarioApp.Colonia,
                    pUsuarioApp.IdMunicipio,
                    pUsuarioApp.Activo
                };
                return sqlcon.QuerySingle<Response>("Sis.prUSUARIOS_APP", values, commandType: System.Data.CommandType.StoredProcedure);
            }
        }*/




        public static CatalogosReservacion prConsultarCatalogosReservacionApp(string pIdUsuarioApp)
        {
            return new CatalogosReservacion()
            {
                lstServicios = prConsultarServicios(),
                lstTipoPago = prConsultarTipoPago(),
                lstEstados = prConsultarEstadosApp(),
                lstDomicilios = prConsultarDomiciliosFavoritos(pIdUsuarioApp)
            };
        }

        public static List<Servicios> prConsultarServicios()
        {
            using SqlConnection sqlcon = new SqlConnection(Persistencia.strCon);
            return sqlcon.Query<Servicios>($"[Cat].[prConsultarServicios]").ToList();
        }

        public static List<TIPO_PAGO> prConsultarTipoPago()
        {
            using SqlConnection sqlcon = new SqlConnection(Persistencia.strCon);
            return sqlcon.Query<TIPO_PAGO>($"[Ser].[prConsultarTipoPago]").ToList();
        }

        public static List<Domicilios> prConsultarDomiciliosFavoritos(string pIdUsuarioApp)
        {
            List<Domicilios> R = new List<Domicilios>();
            Persistencia Per = new Persistencia();

            using (SqlConnection sqlcon = new SqlConnection(Persistencia.strCon))
            {
                sqlcon.Open();

                using (SqlCommand cmndStored = new SqlCommand("[Ser].[prConsultarDomiciliosFavoritos]", sqlcon))
                {
                    cmndStored.CommandType = CommandType.StoredProcedure;
                    cmndStored.Parameters.AddWithValue("@IdUsuarioApp", pIdUsuarioApp);

                    SqlDataReader drResultado = cmndStored.ExecuteReader();

                    if (drResultado.HasRows)
                    {
                        while (drResultado.Read())
                        {
                            Domicilios domicilio = new Domicilios();
                            {
                                var withBlock = domicilio;
                                domicilio.Calle = drResultado["Calle"].ToString();
                                domicilio.Numero = drResultado["Numero"].ToString();
                                domicilio.Colonia = drResultado["Colonia"].ToString();
                                domicilio.Municipio.IdMunicipio = (int)drResultado["IdMunicipio"];
                                domicilio.Municipio.Municipio = drResultado["Municipio"].ToString();
                                domicilio.Estado.IdEstado = (int)drResultado["IdEstado"];
                                domicilio.Estado.Estado = drResultado["Estado"].ToString();
                            }
                            R.Add(domicilio);
                        }
                    }
                    drResultado.Close();
                }
            }

            return R;
        }

        public static List<ESTADOS> prConsultarEstadosApp()
        {
            List<ESTADOS> R = new List<ESTADOS>();
            Persistencia Per = new Persistencia();

            using (SqlConnection sqlcon = new SqlConnection(Persistencia.strCon))
            {
                sqlcon.Open();

                using (SqlCommand cmndStored = new SqlCommand("[Ser].[prConsultarEstadosApp]", sqlcon))
                {
                    cmndStored.CommandType = CommandType.StoredProcedure;
                    cmndStored.Parameters.AddWithValue("@Activo", 1);

                    SqlDataReader drResultado = cmndStored.ExecuteReader();

                    ESTADOS Estado = new ESTADOS();
                    Estado.lstMunicipios = new List<MUNICIPIOS>();

                    if (drResultado.HasRows)
                    {
                        while (drResultado.Read())
                        {
                            MUNICIPIOS Municipio = new MUNICIPIOS();
                            {
                                var withBlock = Municipio;
                                withBlock.IdMunicipio = (int)drResultado["IdMunicipio"];
                                withBlock.Municipio = drResultado["Municipio"].ToString();
                                withBlock.Activo = drResultado["Activo"].ToString();
                            }

                            if (Estado.IdEstado != (int)drResultado["IdEstado"])
                            {
                                Estado = new ESTADOS();
                                Estado.lstMunicipios = new List<MUNICIPIOS>();
                                {
                                    var withBlock = Estado;
                                    withBlock.IdEstado = (int)drResultado["IdEstado"];
                                    withBlock.Estado = (string)drResultado["Estado"].ToString();
                                    withBlock.Activo = drResultado["Activo"].ToString();
                                }

                                R.Add(Estado);
                            }

                            Estado.lstMunicipios.Add(Municipio);
                        }
                    }

                    drResultado.Close();
                }
            }

            return R;
        }

       
    }
}
