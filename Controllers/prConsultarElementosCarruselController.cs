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
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class prConsultarElementosCarruselController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<ELEMENTOS_CARRUSEL>> GET()
        {
            try
            {
                return Ok(ad.prConsultarElementosCarrusel());

            }
            catch (Exception Ex)
            {
                return StatusCode(500, new { error = Ex.Message, result = 1 });
            }
        }
    }
}