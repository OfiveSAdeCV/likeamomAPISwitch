using LoginAPP.AccesoDatos;
using Microsoft.AspNetCore.Mvc;
using NanniesAPI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LoginAPP.Entidades.EntidadesNegocio;

namespace LikeamomAPI.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class prConsultarDatosContactoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<DatosContacto> Get()
        {
            try
            {
                return Ok(ad.prConsultarDatosContacto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response() { Result = 1, Mensaje = ex.Message });
            }
        }
    }
}