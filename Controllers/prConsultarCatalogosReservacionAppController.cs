using LoginAPP.AccesoDatos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LoginAPP.Entidades.EntidadesNegocio;

namespace NanniesAPI.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class prConsultarCatalogosReservacionAppController : ControllerBase
    {
        [HttpGet("{pIdUsuarioApp}")]
        public ActionResult<CatalogosReservacion> GET(string pIdUsuarioApp)
        {
            try
            {
                return Ok(ad.prConsultarCatalogosReservacionApp(pIdUsuarioApp));

            }
            catch (Exception Ex)
            {
                return StatusCode(500, new { Mensaje = Ex.Message, result = 1 });
            }
        }
    }
}