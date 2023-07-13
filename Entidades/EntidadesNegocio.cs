namespace LoginAPP.Entidades
{
    public class EntidadesNegocio
    {

        public class AppSettings
        {
            public string Url { get; set; }
        
            public string Url1 { get; set; }
        }
        public class LogInAppCredentials
        {
            public string Correo { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
            public string DeviceID { get; set; }
            public string BaseURL { get; set; }
        }
        public class Response
        {
            public string Mensaje { get; set; }
            public int Result { get; set; }
            public int Ident { get; internal set; }
        }


        public class ResponseLogIn : Response
        {
            public int IdUsuarioApp { get; set; }
            public int IdPersonal { get; set; }
        }

        public class DatosContacto
        {
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string Facebook { get; set; }
            public string FbPageId { get; set; }
            public string Pagina { get; set; }
            public string Correo { get; set; }

        }
        public class TOKEN_UPDATE
        {
            public int IdUsuarioApp { get; set; }
            public string Token { get; set; }
            public string DeviceID { get; set; }
        }

        public class ELEMENTOS_CARRUSEL
        {
            public int IdElementoCarrusel { get; set; }
            public byte[] Banner { get; set; }
            public string Activo { get; set; }
        }

        public class USUARIOS_APP
        {
            public int IdUsuarioApp { get; set; }
            public string Correo { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Contraseña { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Telefono { get; set; }
            public string TelefonoAdicional { get; set; }
            public string Calle { get; set; }
            public string Numero { get; set; }
            public string Colonia { get; set; }
            public int IdMunicipio { get; set; }
            public string Activo { get; set; }
            public MUNICIPIOS Municipio { get; set; }
            public ESTADOS Estado { get; set; }

            public USUARIOS_APP()
            {
                Municipio = new MUNICIPIOS();
                Estado = new ESTADOS();
            }
        }



        public class CatalogosReservacion
        {
            public List<Servicios> lstServicios { get; set; }
            public List<TIPO_PAGO> lstTipoPago { get; set; }
            public List<ESTADOS> lstEstados { get; set; }
            public List<Domicilios> lstDomicilios { get; set; }
        }


        public class MUNICIPIOS
        {
            public int IdMunicipio { get; set; }
            public string Municipio { get; set; }
            public string Activo { get; set; }
        }



        public class ESTADOS
        {
            public int IdEstado { get; set; }
            public string Estado { get; set; }
            public string Activo { get; set; }
            public List<MUNICIPIOS> lstMunicipios { get; set; }
        }

        public class Domicilios
        {
            public string Calle { get; set; }
            public string Numero { get; set; }
            public string Colonia { get; set; }
            public MUNICIPIOS Municipio { get; set; }
            public ESTADOS Estado { get; set; }
            public Domicilios()
            {
                Municipio = new MUNICIPIOS();
                Estado = new ESTADOS();
            }
        }
        public class Servicios
        {
            public int IdServicio { get; set; }
            public string Servicio { get; set; }
            public string Descripcion { get; set; }
            public int IdPrecioServicio { get; set; }
            public decimal Precio { get; set; }
            public string Activo { get; set; }
        }
        public class TIPO_PAGO
        {
            public int IdTipoPago { get; set; }
            public string TipoPago { get; set; }
            public string Activo { get; set; }
        }

    }
}
